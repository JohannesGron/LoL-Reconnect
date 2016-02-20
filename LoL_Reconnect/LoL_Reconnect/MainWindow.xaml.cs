using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using MahApps.Metro.Controls;
using System.Net;
using System.Runtime.InteropServices;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;

namespace LoL_Reconnect
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        //Used to remember the path of the folder "League of Legends"
        public string lolPath;


        public MainWindow()
        {
            InitializeComponent();

            //Load settings
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\LoL_Reconnect");
            if (key != null) {
                if (key.GetValue("username") != null)
                {
                    username_textbox.Text = key.GetValue("username").ToString();
                }
                if (key.GetValue("region") != null)
                {
                    region_combobox.SelectedIndex = Int32.Parse(key.GetValue("region").ToString());
                }
                if (key.GetValue("path") != null)
                {
                    lolPath = key.GetValue("path").ToString();
                }
            }
            


            //Start internet connection timer
            System.Windows.Threading.DispatcherTimer DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            DispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            DispatcherTimer.Start();
        }


        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            //Check internet connection using Windows Internet API and a DNS test
            if (IsConnected() && DnsTest())
            {
                internet_image.Source = new BitmapImage(new Uri(@"/Images/online.png", UriKind.Relative));
            }
            else
            {
                internet_image.Source = new BitmapImage(new Uri(@"/Images/offline.png", UriKind.Relative));
            }
        }


        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int connDescription, int ReservedValue);

        public static bool IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }


        public static bool DnsTest()
        {
            try
            {
                IPHostEntry ipHe = Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }


        private async void reconnect_button_Click(object sender, RoutedEventArgs e)
        {
            //Fields not empty
            if (!string.IsNullOrEmpty(username_textbox.Text) && !string.IsNullOrEmpty(password_textbox.Password))
            {
                if (region_combobox.SelectedItem != null)
                {
                    //Do we know the lol path
                    if (lolPath != null)
                    {
                        string[] list = Directory.GetDirectories(lolPath + @"RADS\solutions\lol_game_client_sln\releases\");
                        string lolExeFolder = list[0] + @"\deploy";
                        //Got internet connectoin 
                        if (IsConnected() && DnsTest())
                        {
                            //Find relevant login url
                            var item = region_combobox.SelectedItem as System.Windows.Controls.Label;
                            string SelectedItemValue = item.Content.ToString();
                            string loginurl = null;
                            switch (SelectedItemValue)
                            {
                                case "BR": loginurl = Regions.BR; break;
                                case "EUNE": loginurl = Regions.EUNE; break;
                                case "EUW": loginurl = Regions.EUW; break;
                                case "KR": loginurl = Regions.KR; break;
                                case "LAN": loginurl = Regions.LAN; break;
                                case "LAS": loginurl = Regions.LAS; break;
                                case "NA": loginurl = Regions.NA; break;
                                case "OCE": loginurl = Regions.OCE; break;
                                case "RU": loginurl = Regions.RU; break;
                                case "TR": loginurl = Regions.TR; break;
                            }

                            //Login request
                            using (var client = new HttpClient())
                            {
                                var values = new Dictionary<string, string>
                            {
                               { "user", username_textbox.Text},
                               { "password", password_textbox.Password}
                            };

                                var response = await client.PostAsync(loginurl + "/login-queue/rest/queues/lol/authenticate", new FormUrlEncodedContent(values));

                                var responseString = await response.Content.ReadAsStringAsync();

                                JToken json = JObject.Parse(responseString);

                                //If successful login request
                                if (json["inGameCredentials"] != null)
                                {
                                    //Save username and region
                                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\LoL_Reconnect");
                                    key.SetValue("username", username_textbox.Text);
                                    key.SetValue("region", region_combobox.SelectedIndex);
                                    key.Close();

                                    //Check in game state
                                    Boolean inGame = (Boolean)json.SelectToken("inGameCredentials.inGame");
                                    if (inGame)
                                    {
                                        //Close LoL applications
                                        String[] names = { "LoLLauncher", "LoLPatcher", "LolClient", "League of Legends" };

                                        foreach (string value in names)
                                        {
                                            try
                                            {
                                                Process[] process = Process.GetProcessesByName(value);
                                                foreach (Process proc in process)
                                                {
                                                    proc.Kill();
                                                }
                                            }
                                            catch { }
                                        }

                                        //Start League of Legends.exe
                                        string ServerIp = (string)json.SelectToken("inGameCredentials.serverIp");
                                        string ServerPort = (string)json.SelectToken("inGameCredentials.serverPort");
                                        double SummonerId = (double)json.SelectToken("inGameCredentials.summonerId");
                                        string EncryptionKey = (string)json.SelectToken("inGameCredentials.encryptionKey");

                                        var p = new Process
                                        {
                                            StartInfo = {
                                        WorkingDirectory = lolExeFolder,
                                        FileName = lolExeFolder + @"\League of Legends.exe"
                                    }
                                        };
                                        p.StartInfo.Arguments = "\"8394\" \"" + lolPath + "LoLLauncher.exe" + "\" \"" + "\" \"" + ServerIp + " " + ServerPort + " " + EncryptionKey + " " + SummonerId + "\"";
                                        p.Start();
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("You're not in a game");
                                    }
                                }
                                else
                                {
                                    if (json["reason"].ToString() == "invalid_credentials")
                                    {
                                        System.Windows.MessageBox.Show("Invalid credentials or wrong region");
                                    }
                                    else
                                    {
                                        System.Windows.MessageBox.Show("An unknown error has occurred");
                                    }
                                }
                            }
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("No internet connection");
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Please specify your League of Legends path");
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Please specify your region");
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Please fill in all of the fields");
            }
        }


        private void path_button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = System.IO.Path.GetPathRoot(Environment.SystemDirectory);
            dialog.ShowNewFolderButton = false;
            dialog.Description = "Point me to your League of Legends directory - e.g:" + System.Environment.NewLine + @"'C:\Riot Games\League of Legends'";
            System.Windows.MessageBox.Show("Point me to your League of Legends directory - e.g:" + System.Environment.NewLine + @"'C:\Riot Games\League of Legends'");
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                if (File.Exists(path + @"\lol.launcher.exe"))
                {
                    lolPath = path + @"\";

                    //Save path
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\LoL_Reconnect");
                    key.SetValue("path", path);
                    key.Close();
                }
                else if (File.Exists(path + @"\League of Legends\lol.launcher.exe"))
                {
                    lolPath = path + @"\League of Legends\";

                    //Save path
                    RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\LoL_Reconnect");
                    key.SetValue("path", path + @"\League of Legends\");
                    key.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("It's not in here :)");
                }                
            }
        }
    } 
}

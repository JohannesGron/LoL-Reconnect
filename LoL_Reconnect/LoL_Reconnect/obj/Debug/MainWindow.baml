   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   H DLoL_Reconnect, Version=4.2.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:LoL_ReconnectLoL_Reconnect 5   	      LoL_Reconnect.MainWindow   5      HControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro  5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:LoL_Reconnect     5   	     � Title$  LoL Reconnect��5	   	   $��207.712��5
   	   $	��215��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ CanMinimize=�5   	   (   "MahApps.Metro.Controls.MetroWindow   	TitleCaps .     AccentColorBrush   	GlowBrush# �   5   	   .��5      � .+�5   
   �� -   5         ��Name$ username_label��5      $��Username��6(   $
��Left=�6;   $��	10,10,0,0q�6V   $	��Top=�6i   $��0q�6�   ���	#FF4F4F4F6�   �� 5   
   -   5         ��Name$ username_textbox��5      $��	10,31,0,0q�6,    �� TextWrapping$
 Wrap=�6?   $	��Top=�6S   
�������$��18(�6�   $
��Left=�6�   $	��194��6�   �� 5   
   -   5        $ password_label��5      $��Password��6(   $
��Left=�6;   $��	10,73,0,0q�6V   $	��Top=�6i    J� RenderTransformOrigin$ 0.268,0.819&�6�   $��0q�6�   ���	#FF4F4F4F6�   G� 5   
   -   5        	 G�Name$	 password_textbox��5      $��	10,94,0,0q�60   $	��Top=�6C   
�������$��18(�6s   $
��Left=�6�   $	��194��6�   �� 5   
   -   5        
 ��Name$
 reconnect_button��5      $��	Reconnect��6+   $��132,142,0,0q�6?   $	��Top=�6T   )   #MahApps.Metro.Controls.ButtonHelper   PreserveTextCase . $��0q�6�   $��26��6�    �� 	IsDefault . $
��Left=�6�   $��72��6  �� 5   
   -   5         ��Name$ region_combobox��5      $��25��6,   $��
10,142,0,0q�68   $	��Top=�6L    �� 
IsEditable .  �� 
IsReadOnly .  �� Text$ Region��6�   |�.  $
��Left=�6�   $��72��6�   .@�5      �� $��BR��6   
���   �$��66��6J   $��Center=�6U   
 J� Uid$	 asd��6y   ��6!   Z� 6y   �� 5      $
��EUNE��6   
���   �$��66��6L   $��Center=�6W   ��6#   Z� 6W   �� 5      $	��EUW��6   
���   �$��66��6K   $��Center=�6V   ��6"   Z� 6V   �� 5      $��KR��6   
���   �$��66��6J   $��Center=�6U   ��6!   Z� 6U   �� 5      $	��LAN��6   
���   �$��66��6K   $��Center=�6V   ��6"   Z� 6V   �� 5      $	��LAS��6   
���   �$��66��6K   $��Center=�6V   ��6"   Z� 6V   �� 5      $��NA��6   
���   �$��66��6J   $��Center=�6U   ��6!   Z� 6U   �� 5      $	��OCE��6   
���   �$��66��6K   $��Center=�6V   ��6"   Z� 6V   �� 5      $��RU��6   
���   �$��66��6J   $��Center=�6U   ��6!   Z� 6U   �� 5       $��TR��6   
���   �$��66��6J   $��Center=�6U   ��6!   Z� 6U   5!      �� 5"   
   -   5         ��Name$ internet_image��5"      $��	200,3,2,0q�6(   $��10��6;   $	��Top=�6G   �� 5#   
   -   5        $
 path_button��5#      $��34��6&   $��
91,138,0,0q�62   $	��Top=�6F     CustomStyle#���  6^   $
��Left=�6�   $��34��6�   $��0q�6�   ��5$      �� 5%       �� ImageSource$ Images/gear.png��6    �� RelativeTransform 5&      V� 5'      .~�5(      ��  �� CenterY$	 0.5Q�6-    �� CenterX$	 0.5Q�6;    �� ScaleX$	 0.8Q�6I    �� ScaleY$	 0.8Q�6V   �� 5)       �� CenterY$	 0.5Q�6,    �� CenterX$	 0.5Q�6:   �� 5*       �� CenterY$	 0.5Q�6.    �� CenterX$	 0.5Q�6<   T� 5+      5,      5.      50      51       � 	Resources 52       �� 
TargetType  AccentedSquareButtonStyle �� BasedOn  �� Property! �� Value" �� 
TargetType# �� Triggers$ P� Property% P� Value  MetroCircleButtonStyle& A�Name  
BlackBrush' J� Opacity( ��Name) J� SnapsToDevicePixels* �� Triggers+ �� 
TargetName��%�  '��       7  &	 G    7  �� 53   
   " ��6   foo��8   .�55      ��   � ��! 56      �� 57      "" ��6&   .R�58      �� #��z��6!   ._�59      �� $��Center=�6/   $��Center=�6L   5:      5;      5=      # 5>      P� 5?      $ �@J�IsMouseOver% . . �5@      ��   � ��! �	#CC088AC163   5A      5C      �� 5D   
   " ��6$   8   .�5F      ��   � ��! 5G      �� 5H      "" ��6&   .R�5I      � .+�5J      A� $��4q�6&   #��z��61   $��0��6U   A� 5K      $& 	ellipsebg��6&   $��4q�5L   "   #���  5M   "   $��0��5N   "   $' 0Q�5O   "   �� 5P      $( PART_ContentPresenter��6/   #��z��5Q   +   #��z��5R   +   #��z��5S   +   ��. #) z) 5U   +   5V      * 5W      P� 5X      $ �@J�IsMouseOver% . . �5Y   "   �� $+ 	ellipsebg��6)     �@J�Opacity$
! 0.15Q�6S   5Z      P� 5\      $ � ��% . . �5]   "   �� $+ 	ellipsebg��6)     �@J�Opacity$	! 0.3Q�6S   5^      5`      5b      5c      5d      5e      
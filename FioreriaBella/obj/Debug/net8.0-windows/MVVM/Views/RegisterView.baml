   M S B A M L   `   `   `  ����  FioreriaBella+    %FioreriaBella.MVVM.Views.RegisterView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  RegisterPage��5       N� 
Background
 ���� .��5      � .+�5      �� ���	#FFFDF5F0 �� CornerRadius$ 20��5	       �� Padding$ 25q�5
      $��3q�5      
���E��$��Center=�5      $��Center=�5      $	��420��5       J� Opacity$
 0.97Q�5      ._�5      �� .+�5   
   �� $��Crea un account��6   $��22(�5      $��Palatino Linotype)�5      $
��Bold"�5      
���/kU�$��Center=�5      $��0,0,0,12q�5      �� 5   
   $%��Имя пользователя��6   $��15(�5      $��Palatino Linotype)�5      
���E��$��0,0,0,1q�5       �� 5!   
   -   5         ��Name$ UsernameBox��5!      $��0,1,0,0q�5"      $��32��5#      $	��6,3q�5$         ElegantInputStyle#��[  5%       � FocusVisualStyle 5&      Z� ��5'      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=��� 5(   
   $��12(�5)      $��0,1,0,5q�5*      $��16��5+      $y�Visible=�5-      ��5(      �� *UsernameValidationColor+��5,      �� *UsernameValidationHint+5-      �� 5/   
   $��Email��6   $��15(�50      $��Palatino Linotype)�51      
���E��$��0,0,0,1q�53      �� 54   
   -   5        $ EmailBox��54      $��0,1,0,0q�55      $��32��56      $	��6,3q�57      #��[  58       59      Z� ��5:      �� *Email+$ PropertyChanged=��� 5;   
   $��12(�5<      $��0,1,0,5q�5=      $��16��5>      $y�Visible=�5@      ��5;      �� *EmailValidationColor+��5?      �� *EmailValidationHint+5@      �� 5B   
   $��Пароль��6   $��15(�5C      $��Palatino Linotype)�5D      
���E��$��0,0,0,1q�5F      G� 5G   
   -   5         G�Name$ PasswordBox��5G      $��0,1,0,0q�5H      $��32��5I      $	��6,3q�5J        ElegantPasswordStyle#��[ 5K       5L      Z� 5M      �� 5N   
   $��12(�5O      $��0,1,0,10q�5P      $��16��5Q      $y�Visible=�5S      ��5N      �� *PasswordValidationColor+��5R      �� *PasswordValidationHint+5S      �� 5U   
   $)��#Подтвердите пароль��6   $��15(�5V      $��Palatino Linotype)�5W      
���E��$��0,0,0,1q�5Y      G� 5Z   
   -   5        $ ConfirmPasswordBox��5Z      $��0,1,0,0q�5[      $��32��5\      $	��6,3q�5]      #��[ 5^       5_      Z� 5`      �� 5a   
   $��12(�5b      $��0,1,0,10q�5c      $��16��5d      $y�Visible=�5f      ��5a      �� * ConfirmPasswordValidationColor+��5e      �� *ConfirmPasswordValidationHint+5f      �� 5h   
   
���  ��$��12(�5i      $��0,0,0,10q�5j      $
��Wrap=�5l      ��5k      �� *ErrorMessage+5l      �� 5n   
   $��Регистрация��6   $	��150��5o      $��38��5p      $��Center=�5q        ElegantButtonStyle#��[ 5r      $��0,0,0,6q�5s      ��5t      �� *RegisterCommand+��5u      �� 	 �� ElementName$	 ConfirmPasswordBox���� 5w   
   $��
Войти��6   $	��150��5x      $��38��5y      $��Center=�5z        OutlinedButtonStyle#��[ 5{      ��5|      �� *NavigateToLoginCommand+5}   	   5~      5      5�      
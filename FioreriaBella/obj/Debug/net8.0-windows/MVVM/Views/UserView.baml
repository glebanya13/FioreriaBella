   M S B A M L   `   `   `  ����  FioreriaBella'    !FioreriaBella.MVVM.Views.UserView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  UserPage��5       N� 
Background
 ���� .��5      � 
������ $��20q�6"   .+�5      �� ���	#FFFDF5F0 �� CornerRadius$ 20��5	       �� Padding$ 30q�5
      $��3q�5      
���/kU�$��Center=�5      $��Center=�5      $	��600��5       J� Opacity$
 0.97Q�5      ._�5      �� .+�5   
   �� $��28(�5      $��Georgia)�5      $
��Bold"�5      
��� d �$��Center=�5      $��0,0,0,20q�5      ��5      �� *
Username+ �� StringFormat$+ %Добро пожаловать, {0}!��5      �� 5   
   $��0,0,0,30q�6   .+�5      �� $0��*Информация об аккаунте��6   $��18(�5      $��SemiBold"�5      
���E��$��0,0,0,10q�5!      � 5#       � ColumnDefinitions 5$      �� 5%      $	��120 �6!   �� 5&      $��* �6!   .+�5)      �� $&�� Имя пользователя:��6   $
��Bold"�5*      
���/kU�$��0��5,      �� 5-      $��1��5.      $��Italic$�5/      ��5-      �� *
Username+5/      �� 51      $��Email:��6   $
��Bold"�52      
���/kU�$��0��54      $��0,25,0,0q�55      $	��Top=�56      �� 57      $��1��58      $��0,25,0,0q�59      $��Italic$�5:      $	��Top=�5;      ��57      �� *Email+5;      5<      5=      �� 5?   
   $w��qЗдесь вы можете управлять своей корзиной и списком желаемого.��6   $��16(�5@      $��Palatino Linotype)�5A      
���E�� �� TextAlignment$ Center=�5C      $
��Wrap=�5D      $��0,0,0,25q�5E      �� 5G   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   .+�5I      �� $��Корзина��6      ElegantButtonStyle#��[  5J      $	��140��5K      $��38��5L      $	��5,0q�5M      ��5N      �� *OpenCartCommand+�� 5P      $��WishList��6   #��[  5Q      $	��140��5R      $��38��5S      $	��5,0q�5T      ��5U      �� *OpenWishlistCommand+5W      �� 5Y   
   $��
Выйти��6     OutlinedButtonStyle#��[ 5Z      $	��140��5[      $��38��5\      $��Center=�5]      $��0,30,0,0q�5^      ��5_      �� *LogoutCommand+5`   	   5a      5b      5c      
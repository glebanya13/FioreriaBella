   M S B A M L   `   `   `  ����H  DFioreriaBella, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:FioreriaBella.MVVM.ViewModelsFioreriaBella.MVVM.ViewModels  5      (    "FioreriaBella.MVVM.Views.AdminView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :vm+clr-namespace:FioreriaBella.MVVM.ViewModels     5        N� Title$  Админ-панель��5      .��5      �� $
��Auto=�6   .��5      � 
������ .+�5	      �� ���	#FFFDF5F0 �� CornerRadius$ 20��5
       �� Padding$ 30q�5      $��3q�5      
���E��$��Center=�5      $��Center=�5      $	��600��5       J� Opacity$
 0.97Q�5      ._�5   
   �� $��Center=�6   $	��500��62   .+�5      �� $/��)Панель администратора��6   $��24(�5      $
��Bold"�5      
���E��$��Center=�5      $��0,0,0,10q�5      �� 5      $��16(�5      $
��Bold"�5      
���/kU�$��Center=�5       $��0,0,0,5q�5!      ��5      �� *
Username+5!      �� 5#      $��14(�5$      
���/kU�$��Center=�5&      $��0,0,0,15q�5'      ��5#      �� *Email+5'      �� 5)      $����Добро пожаловать в панель администратора. Здесь вы можете управлять пользователями, товарами и заказами.��6   $��14(�5*       �� TextAlignment$ Center=�5+      $
��Wrap=�5,      $��0,0,0,20q�5-      � 5/      $��Center=�6   $��0,0,0,10q�63   .+�50      �� $��Пользователи��6      ElegantButtonStyle#��[  51      $	��150��52      $��10q�53      ��54      �� *ManageUsersCommand+�� 55      $��Товары��6   #��[  56      $	��150��57      $��10q�58      ��59      �� *ManageProductsCommand+�� 5:      $��Заказы��6   #��[  5;      $	��150��5<      $��10q�5=      ��5>      �� *ManageOrdersCommand+5?      �� 5A      $��	0,20,0,20q�6   �� 5C      $��
Выйти��6     OutlinedButtonStyle#��[ 5D      $	��200��5E      $��Center=�5F      ��5G      �� *LogoutCommand+5H      5I   	   5J      5K      5L      
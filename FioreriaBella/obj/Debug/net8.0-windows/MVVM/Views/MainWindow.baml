   M S B A M L   `   `   `  ����H  DFioreriaBella, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:FioreriaBella.MVVM.ConvertersFioreriaBella.MVVM.Converters  5   	   )    #FioreriaBella.MVVM.Views.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   <conv+clr-namespace:FioreriaBella.MVVM.Converters     5   	     � Title$  Fioreria Bella��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	   ���	#FFF8F0E3 � WindowState$ 	Maximized=�5
   	    � 	Resources 5       �� MergedDictionaries �� Source=   7FioreriaBella.MVVM.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter�� 5       5      �� 5   
   $O Ipack://application:,,,/FioreriaBella;component/MVVM/Resources/Styles.xaml?�6   %   &	          5      6'   5      .��5      �� 
��� ���$��4q�5      ���	#FFF8F0E3 J� SnapsToDevicePixels$
 True��5      ._�5      � .+�5      �� $R��Lpack://application:,,,/FioreriaBella;component/MVVM/Resources/Background.jpg��6   $��UniformToFill=�5      	 J� Opacity$		 0.8Q�5      
 K� ZIndex$
 0��5      � 5       -   5        $��40��5       ���	#FFEED6A0$	��Top=�5"      $
 2��5$      .+�5%   
   �� $��Fioreria Bella��6   $��Georgia)�5&      $��18(�5'      $
��Bold"�5(      
���E��$��Center=�5*      $��15,0,0,0q�5+      �� 5,   
   -   5        $��×��5,      $��40��5-      $��40��6   
������ $��0q�5/      $��18(�50      $
��Bold"�51      
���  �� � Cursor$
 Hand��53      $��Right=�55      56   	   �� 58      ���	#FFF3E6BE$��50��59      $	��Top=�5:      $��0,40,0,0q�5;      $
 2��5<      y�5=      �� *IsNavigationVisible+ �� 	Converter# [  ._�5>   
   ��  �� Orientation$ 
Horizontal=�6   $��Center=�5?      $��Center=�5@      .+�5A      �� $��Профиль��6   $	��120��5B      $��35��6   $
��10,7q�5C        NavButtonStyle#��[ 5D      ��5E      �� *NavigateProfileCommand+�� 5F      $��Каталог��6   $	��120��5G      $��35��6   $
��10,7q�5H      #��[ 5I      ��5J      �� *NavigateCatalogCommand+�� 5K      $��
Выйти��6   $	��120��5L      $��35��6   $
��10,7q�5M      #��[ 5N      ��5O      �� *ExitCommand+5P      5Q   	    � 5S      -   5          �Name$ 	MainFrame��5S      $��0,90,0,0q�5T        � NavigationUIVisibility$ Hidden=�5U      
������ $
 1��5W      5X      5Y      5Z      
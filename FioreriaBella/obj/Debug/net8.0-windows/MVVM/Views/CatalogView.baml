   M S B A M L   `   `   `  ����H  DFioreriaBella, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:FioreriaBella.MVVM.ViewModelsFioreriaBella.MVVM.ViewModels  5      *    $FioreriaBella.MVVM.Views.CatalogView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :vm+clr-namespace:FioreriaBella.MVVM.ViewModels     5        N� Title$  Catalogo��5       � DataContext 5      4   .FioreriaBella.MVVM.ViewModels.CatalogViewModel  5      .��5      � .+�5      ��  �� Padding$ 20q�6   
���/kU�$��2q�5       �� CornerRadius$ 12��5      $��30q�5      ���	#FFFDF5F0 J� SnapsToDevicePixels$
 True��5      ._�5      �� .+�5   
   �� $��← Indietro��6   $	��100��5      $��0,0,0,15q�5      $
��Left=�5         ElegantButtonStyle#��[  5      ��5      �� *BackCommand+�� 5   
   $��Catalogo Fiori Freschi��6   $��26(�5      $��Georgia)�5      $
��Bold"�5       
���/kU�$��Center=�5"      $��0,0,0,20q�5#      �� 5%   
   $<��6Sfoglia la nostra selezione di fiori freschi e piante.��6   $��16(�5&      $��Palatino Linotype)�5'      
���E��$
��Wrap=�5)      $��0,0,0,20q�5*      �� 5,   
   $
��Auto=�6   $��Disabled=�5-      .��5.      �� $��Stretch=�5/      ��5.      �� *
Products+��50      �� 51      .R�52      �  � Orientation$ 
Horizontal=�6   53      ��56      �� 57      .R�58      �� 
��� ���$��1q�61   $ 8��6E   $ 10q�6V   $��8q�6c   
�������$	��200��6�   $	��220��6�   ._�59      ��  �� Orientation$ Vertical=�6   $	��Top=�66   .+�5;      �� $ 
Horizontal=�6!   $��0,0,0,5q�6:   .+�5<      �� $��60��6:   $��60��6E   $��0,0,10,0q�6Q   ��6   �� *
ImageUrl+6Q   �� 5=      $	��130��6#   .+�5>      �� $
��Bold"�5?   $   $��14(�66   ���#4B2E2B$��CharacterEllipsis=�5@   $   $��NoWrap=�5A   $   ��5>   $   �� *Name+5A   $   �� 5B      $��SemiBold"�5C   $   
���/kU�$��0,5,0,0q�6V   ��5B   $   �� *Price+ �� StringFormat$ 	€{0:F2}��5C   V   5D      5E      �� 5G      $��Italic$�5H       ���#4B2E2B$
��Wrap=�5I       $��11(�64   $��48��5J       $��WordEllipsis=�5K       ��5G       �� *Description+5K       �� 5M      $ 
Horizontal=�6!   $��Center=�6:   $��0,5,0,0q�6W   .+�5N      �� $��Купить��6   #��[  5O      $��70��5P      $��24��6*   $��0,0,5,0q�5Q      $��11(�5R      ��5S      �� *DataContext.AddToCartCommand+ �� RelativeSource �� 	 �� AncestorType	 Page��5T      �� �� 5V      $(��"В список желаемого��6   #��[  5W      $	��120��5X      $��24��6+   $��11(�5Y      ��5Z      �� *" DataContext.AddToWishlistCommand+ �� 	 Page��5[      �� 5\      5^      5_      5`      5c      5d      5f   	   5g      5h      5i      
   M S B A M L   `   `   `  ����B  >Popcorn, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:PopcornPopcorn  5   	   ?$clr-namespace:Popcorn.MVVM.ViewModelPopcorn.MVVM.ViewModel  5   	   5clr-namespace:Popcorn.MVVM.ViewPopcorn.MVVM.View  5   	       Popcorn.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Popcorn     5   	   :	viewModel$clr-namespace:Popcorn.MVVM.ViewModel     5   	   0viewclr-namespace:Popcorn.MVVM.View     5   	   $	��600��5
   	   $	��920��5   	     � WindowStyle$
  None=�5   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      *  $Popcorn.MVVM.ViewModel.MainViewModel  5   
   .��5      �� ���#272537 �� CornerRadius$ 20��5   	   ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��auto �6    �� 5       $��* �6    .+�5#      �� -   5         ��Name$
 Logo��5#      $��0��5$      $��0��5%       �� Orientation$ 
Horizontal=�5&      $��10q�5'      .+�5(      �� $g��aD:\ATM\BazeDate\vsProjects\MultiThread_ClientServer\BD App\old\Popcorn\Popcorn\Images\popcorn.png��6   $��60��5)      $��60��6   �� 5*      $��Popcorn��6   $��45(�5+      $��Center=�5,      $
��Left=�5-      $��10q�5.      
�������$
��Bold"�67   $��Italic$�6I   5/      �� 51      $��1��52      .+�54      	� $
��Home��55      $��50��56      
�������$��14(�58         MenuButtonTheme#��[  59      	 d� 	IsChecked$
	 TrueY�5:      ��5;      �� *HomeViewCommand+	� 5=      -   5        $��Top 10��5>      $��50��5?      
�������$��14(�5A      #��[  5B      ��5C      �� *HomeViewCommand+69   	� 5E      $��Featured��5F      $��50��5G      
�������$��14(�5I      #��[  5J      ��5K      �� *DiscoveryViewCommand+�� 5M      -   5        
 ��Name$
 CategoryComboBox��5N      $��10q�5O      $
��auto��5P      $��35��5Q      5R      &   System.Windows.Controls.Calendar  5U      -   5          Name$ Calendar��5V      $��40q�5W      5Y      �� 5[      $��0��5\      $��1��5]      $
��Left=�5^      $��Center=�5_      $ 
Horizontal=�5`      .+�5a      �� $	��250��5b      $��Center=�5c      $��5,18,-10 ,17q�5d        ModernTextBox#��[ 5e      �� 5f      $��50��5g      $��50��5h      $��Center=�5i      $��Center=�5j      5k      �� 5m      $��1��5n      $��1��5o      $��10,10,10,10q�5p      ��5q      �� *CurrentView+5s      5u      5v      5x      
   M S B A M L   `   `   `  ����  HOWKTEAM_WPF    HOWKTEAM_WPF.Bai36   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localself:clr-namespace:HOWKTEAM_WPF     5   	     � Title$  Bai36��5   	   $	��450��6   $	��800��6$    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$ self:CustomCommands.Exit��5
      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   o� .@�5      m� $
��File��6   .@�5      m�  m� Command$ self:CustomCommands.Exit��6   5      5      �� 5   
   $��1��6   $��Center=�6"   $��Center=�6?   .+�5      �� $��self:CustomCommands.Exit��6   .��68   Exit6>   5      5      5      
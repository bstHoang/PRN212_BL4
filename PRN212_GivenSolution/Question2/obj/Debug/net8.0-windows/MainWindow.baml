   M S B A M L   `   `   `  ����D  @Question2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Question2	Question2  5   	       Question2.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Question2     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      .+�5   
   �� $��0��6    �� Orientation$ Vertical=�6"   .+�5      �� -   5         ��Name$ cbFilterDepartment��5      $	��200��63   $
��Left=�5       �� DisplayMemberPath$ DepartmentName��5       �� SelectedValuePath$ DepartmentId��5      $��10q�5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ dataGridEmployee��5      $	��160��5         AutoGenerateColumns$ False��5      $��Stretch=�5      $��Stretch=�5      $��10q�5      $��0��5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 FullName��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6;   �� *
FullName+6X     5      $	 PositionName��6)   $
 * 6i    6?   �� *Position.PositionName+6i     5       $	 DepartmentName��6)   $
 * 6o    6A   �� *Department.DepartmentName+6o     5!      $	 
DateOfHire��6)   $
 * 6\    6=   �� *
DateOfHire+6\     5"      $	 DateOfBirth��6)   $
 * 6^    6>   �� *DateOfBirth+6^   5$      5%      �� 5&   
   $��1��6   $ Vertical=�6"   .+�5'      �� $��Add New Employee��6   $
��Bold"�6/   �� 5(      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5)      �� $��	Full Name��6   $	��100��6,   �� 5*      -   5        $��txtName��5*      $	��170��6)   5+      �� 5,      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�5-      �� $��Date of Birth��6   $	��100��60   (  "System.Windows.Controls.DatePicker  5.      -   5        $��dpkDOB��5.      $	��170��6+   5/      �� 50      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�51      �� $��Date of Hire��6   $	��100��6/     52      -   5        $��dpkdoh��52      $	��170��6+   53      �� 54      $ 
Horizontal=�6   $��0,10,0,0q�62   .+�55      �� $��Position��6   $	��100��6+   �� 56      -   5        $��cbPositionId11��56      $	��170��61   $ PositionName��57      $ 
PositionId��6<   58      � 59      $��10q�6    � ColumnDefinitions 5:      �� 5;      $	��100 �6'   .+�5=      �� -   5        $��	btnInsert��5=   )   $��0��6   $��Add Employee��6:   $��0,0,5,0q�6Q   6b   5>      5?      5@      5A      
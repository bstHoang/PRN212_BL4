   M S B A M L   `   `   `  ����C  ?ListView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ListViewListView  5   	       ListView.Window18   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:ListView     5   	     � Title$  Window18��5   	   $	��450��6   $	��800��6'   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ listViewStudents��5      $��Stretch=�5      $��Stretch=�5      $��10q�5      $��0��5       �� View 5      �� 5      .P�5      �� $��Id��6%    �� Width$ 50��6U    �� DisplayMemberBinding 61   �� *Id+6U   �� 5      $
��Name��6%   $	 150��6Y    63   �� *Name+6Y   �� 5      $��Gender��6%   $	 100��65    �� CellTemplate 5      �� 5      .R�5   "   �� ��5   &   �� 5   *    �� 
TargetType" ��60   .�5   .   ��  �� Property � ��	 �� Value$
	 Nữ��6E   
 �� Triggers
 5   .   �� 5   2    �� Binding 6>   �� *Gender+ �� Value True6Y   .a�5    6   ��  � ��$		 Nam��6M   5!   3   5#   +   5%   #   5&      5(      �� 5*      $��DepartId��6%   $	 150��6d    67   �� *Depart.Name+6d   �� 5+      $	��Dob��6%   $	 122��6W    62   �� *Dob+6W   �� 5,      $	��Gpa��6%   $ 50��6W    62   �� *Gpa+6W   5-      5/      �� 50   
   $��1��6   $��10q�6%   .+�52      �� $��Filter by Gender:��6   $
��Bold"�61   $��0,0,0,5q�6C   �� 53      -   5         ��Name$ comboGender��53      $��0,0,0,10q�6,   .@�54      �� $��
Tất cả��6   
 � Tag$	 All��60    �� 
IsSelected$
 True��6:   �� 55      $	��Nam��6   $
 True��6-   �� 56      $
��Nữ��6   $ False��6,   57      �� 5:      $��Filter by Department:��6   $
��Bold"�65   $��0,0,0,5q�6G   �� 5;      -   5         ��Name$ stackDepartments��5;      5=      �� 5@      -   5        $��Filter��5@      $��0,10,0,0q�6A   5A      5C      5D      
   M S B A M L   `   `   `  ����P  LProject_PRN212_FALL24, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Project_PRN212_FALL24Project_PRN212_FALL24  5   	   '    !Project_PRN212_FALL24.UserHistory   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Project_PRN212_FALL24     5   	     � Title$  UserHistory_HoangBST��5   	   $	��700��6&   $
��1400��63    � WindowStartupLocation$ CenterScreen=�5	   	   ���#2C3E50 � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	   .��5      � .+�5   
   �� -   5        $��T��5      $��24(�6   $
��Bold"�6+   
����������#1ABC9C �� TextAlignment$ Center=�5   "   $	��244��69   $��50��6E   $	��Top=�5      $
��Left=�6$   $��	10,10,0,0q�6?    � Cursor$
 Hand��5      6   �� 5   
   -   5        $��X��5      $��30��6   $	��Top=�6)   $��16(�5      $
��Bold"�6   
������ 
�������
������ $
 Hand��5   :   $��
1355,0,0,0q�5       ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 �����63   �� 5       � ��
 ���� 63   �� 5       �@�Cursor$
 Hand��6/   	 �� Triggers	 5      P� 5      
 P� Property
 �@J�IsMouseOver P� Value$
 True��69   . �5      ��  � �� �#E74C3C6;   5       5"      5$      A� 5'   
   -   5        $��50��5'      $��50��6   ���#3498DB$	��Top=�68   $��Right=�6P   $��	0,10,50,0q�6l   $
 Hand��5(   (   � 5+   
   -   5         �Name$ ProfilePopup��5+      $��Mouse=�6&    � 	StaysOpen$
 True��68   .��5,      �� 
������� �� CornerRadius$ 10��6(    �� Padding$ 10q�6:   $	��200��6G   ._�5-      �� .+�5.      �� -   5         ��Name$ EmailTextBlock��5.       $��14(�68   $
��Bold"�6G   $��0,0,0,10q�6Y   �� 5/      -   5        $��Thông tin tài khoản��5/       $��10q�6;   $��14(�6G   $
 Hand��6U   6c   �� 50      -   5        $��Lịch sử mượn sách��50       $��10q�69   $��14(�6E   $
 Hand��6S   6a   �� 51      -   5        $ AdminAccountTextBlock��51       $��Tài khoản Admin��6?   $��10q�6V   $��14(�6b   $
 Hand��6p   6~   �� 52      -	   5        $��Đăng xuất��52       $��10q�61   $��14(�6=   $
 Hand��6K   6Y   53      54      55      �� 57   
   ���#ECF0F1$
��1423��6&   $��80��63   $	��Top=�6?   $
��Left=�6W   $��
-17,65,0,0q�6r   $
 Hand��6�   ._�58      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   $��Center=�6M   $��10,0,0,0q�6h   .+�5:      �� $��From Date Loan��6   $��	10,0,10,0q�61   (  "System.Windows.Controls.DatePicker  5;      -
   5          Name$ DatePickerFrom��5;      $	��150��65   $��	10,0,10,0q�6A   �� 5=      $��To Date Loan��6   $��	10,0,10,0q�6/     5>      -   5        $ DatePickerTo��5>      $	��150��63   $��	10,0,10,0q�6?   �� 5@      -   5        $��Search��5@      $	��100��6*   66   5A      5B      �� 5D   
   ���#ECF0F1$��
10,161,0,0q�6&   $ 10��6:   ._�5E      �� $��20q�6   .+�5F      �� $��Lịch sử mượn sách��6   $��18(�65   $
��Bold"�6C   ���#34495E$��0,0,0,20q�5G      � 5I       � RowDefinitions 5J      �� 5K      $
��212* �6(   �� 5L      $
��213* �6(    � ColumnDefinitions 5O      �� 5P      $��4* �6+   �� 5R      $��1* �6+   .+�5W      &   System.Windows.Controls.DataGrid  -   5          Name$ BookDataGrid��5W         CanUserAddRows$ False��65      AutoGenerateColumns$ False��5X   !   $��Center=�5Y   !   $	��794��5Z   !   $��2��6-    J� RenderTransformOrigin$ 0.509,0.509&�6>   $��0,-18,0,-416q�6b      Columns 5[      0  *System.Windows.Controls.DataGridTextColumn  5\      ,  &System.Windows.Controls.DataGridColumn   Header$ Id Book��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6j   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *IdbookNavigation.Id+6j     5]      $ 
Tên Sách��61   $ * 6m    6C   �� *IdbookNavigation.Name+6m     5^      $ Ngày Mượn��61   $ 2* 6e    6D   �� *BookLoanDate+6e     5_      $ Ngày Trả��61   $ * 6f    6C   �� *BookReturnDate+6f   5a      5d      5e      5g      5h      5i      
using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace ListView
{
    /// <summary>
    /// Interaction logic for Window8.xaml
    /// </summary>
    public partial class Window8 : Window
    {
        public Window8()
        {
            InitializeComponent();
            LoadStudent();
        }
        private void LoadStudent()
        {
            using (var context = new Prn212Bl14Context())
            {
                var studentList = context.Students.Include(x => x.Depart).ToList();
                listViewStudents.ItemsSource = studentList; // Sử dụng ListView thay vì DataGrid
            }
        }
        private void GenderCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox selectedCheckBox)
            {
                // Bỏ chọn tất cả các CheckBox khác
                foreach (var child in stackPanelGender.Children)
                {
                    if (child is CheckBox checkBox && checkBox != selectedCheckBox)
                    {
                        checkBox.IsChecked = false;
                    }
                }

                // Lọc sinh viên theo giới tính
                string selectedGender = selectedCheckBox.Tag.ToString();
                using (var context = new Prn212Bl14Context())
                {
                    var students = context.Students.Include(x => x.Depart).AsQueryable();

                    if (selectedGender == "Male")
                    {
                        students = students.Where(x => x.Gender == true);
                    }
                    else if (selectedGender == "Female")
                    {
                        students = students.Where(x => x.Gender == false);
                    }

                    // Nếu chọn "All", không áp dụng bộ lọc
                    listViewStudents.ItemsSource = students.ToList();
                }
            }
        }
    }
}
using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window9.xaml
    /// </summary>
    public partial class Window9 : Window
    {
        public Window9()
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
        private void ComboBoxGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxGender.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedGender = selectedItem.Tag.ToString();

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
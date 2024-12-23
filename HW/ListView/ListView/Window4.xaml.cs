using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace ListView
{
    /// <summary>
    /// Interaction logic for Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
            LoadStudent();
            LoadDepartmentsComboBox();
        }

        private void LoadStudent()
        {
            using (var context = new Prn212Bl14Context())
            {
                var studentList = context.Students.Include(x => x.Depart).ToList();
                listViewStudents.ItemsSource = studentList; // Sử dụng ListView thay vì DataGrid
            }
        }
        private void LoadDepartmentsComboBox()
        {
            using (var context = new Prn212Bl14Context())
            {
                var departments = context.Departments.ToList();

                // Thêm tùy chọn "All" vào danh sách
                departments.Insert(0, new Department { Id = "All", Name = "All Departments" });
                cbFilterDepartment.ItemsSource = departments;

                cbFilterDepartment.SelectedIndex = 0; // Mặc định chọn "All"
            }
        }


        private void cbFilterDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new Prn212Bl14Context())
            {
                var selectedDepartment = cbFilterDepartment.SelectedItem as Department;

                // Nếu chọn "All", hiển thị toàn bộ sinh viên
                var filteredStudents = selectedDepartment?.Id == "All"
                    ? context.Students.Include(x => x.Depart).ToList()
                    : context.Students.Include(x => x.Depart).Where(x => x.DepartId == selectedDepartment.Id).ToList();

                listViewStudents.ItemsSource = filteredStudents;
            }
        }
    }
}

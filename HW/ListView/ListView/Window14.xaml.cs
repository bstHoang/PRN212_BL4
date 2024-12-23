using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window14.xaml
    /// </summary>
    public partial class Window14 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window14()
        {
            InitializeComponent();
            _dbContext = new Prn212Bl14Context();

            // Load dữ liệu từ database
            LoadData();
        }

        private void LoadData()
        {
            // Lấy danh sách Department
            var departments = _dbContext.Departments.ToList();

            // Thêm lựa chọn "All Departments" vào ComboBox
            departments.Insert(0, new Department { Id = "", Name = "All Departments" });

            cmbDepartments.ItemsSource = departments;

            // Lấy danh sách Student
            var students = _dbContext.Students.Include(s => s.Depart).ToList();
            listViewStudents.ItemsSource = students;
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy các giá trị filter
            bool? isMaleChecked = chkMale.IsChecked;
            bool? isFemaleChecked = chkFemale.IsChecked;

            var selectedDepartment = cmbDepartments.SelectedValue?.ToString();
            // Lọc dữ liệu trực tiếp từ database
            var filteredStudents = _dbContext.Students
                .Include(s => s.Depart) // Include Department
                .Where(student =>
                    // Filter Gender
                    ((isMaleChecked == true && student.Gender == true) ||
                     (isFemaleChecked == true && student.Gender == false)) &&
                    // Filter Department
                    (string.IsNullOrEmpty(selectedDepartment) || student.DepartId == selectedDepartment))
                .ToList();

            // Cập nhật ListView
            listViewStudents.ItemsSource = filteredStudents;
        }

    }
}
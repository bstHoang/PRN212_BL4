using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace ListView
{
    /// <summary>
    /// Interaction logic for Window13.xaml
    /// </summary>
    public partial class Window13 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window13()
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
            foreach (var department in departments)
            {
                // Thêm CheckBox cho từng Department
                var checkBox = new RadioButton()
                {
                    Content = department.Name,
                    Tag = department.Id,
                    GroupName = "Departments"
                };
                departRadioButtons.Children.Add(checkBox);
            }

            // Lấy danh sách Student
            var students = _dbContext.Students.Include(s => s.Depart).ToList();
            listViewStudents.ItemsSource = students;
        }
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy các giá trị filter
            bool? isMaleChecked = chkMale.IsChecked;
            bool? isFemaleChecked = chkFemale.IsChecked;

            var selectedDepartment = departRadioButtons.Children
        .OfType<RadioButton>()
        .FirstOrDefault(rb => rb.IsChecked == true)?.Tag?.ToString();
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

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ListView.Models;
using Microsoft.EntityFrameworkCore;

namespace ListView
{
    public partial class Window12 : Window
    {
        private Prn212Bl14Context _dbContext;


        public Window12()
        {
            InitializeComponent();
            // Khởi tạo DbContext
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
                var checkBox = new CheckBox
                {
                    Content = department.Name,
                    Tag = department.Id
                };
                departCheckBoxes.Items.Add(checkBox);
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

            var selectedDepartments = departCheckBoxes.Items.OfType<CheckBox>()
                .Where(cb => cb.IsChecked == true)
                .Select(cb => cb.Tag.ToString())
                .ToList();

            // Lọc dữ liệu trực tiếp từ database
            var filteredStudents = _dbContext.Students
                .Include(s => s.Depart) // Include Department
                .Where(student =>
                    // Filter Gender
                    ((isMaleChecked == true && student.Gender == true) ||
                     (isFemaleChecked == true && student.Gender == false)) &&
                    // Filter Department
                    (selectedDepartments.Count == 0 || selectedDepartments.Contains(student.DepartId)))
                .ToList();

            // Cập nhật ListView
            listViewStudents.ItemsSource = filteredStudents;
        }

    }
}

using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window15.xaml
    /// </summary>
    public partial class Window15 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window15()
        {
            InitializeComponent();
            _dbContext = new Prn212Bl14Context();

            // Load dữ liệu từ database
            LoadData();
        }

        private void LoadData()
        {
            // Lấy danh sách Department từ database
            var departments = _dbContext.Departments.ToList();

            foreach (var department in departments)
            {
                // Tạo CheckBox cho mỗi Department
                var checkBox = new CheckBox
                {
                    Content = department.Name,
                    Tag = department.Id, // Lưu Id của Department để lọc
                    Margin = new Thickness(0, 5, 0, 5)
                };
                departCheckboxes.Items.Add(checkBox);
            }

            // Lấy danh sách Student
            var students = _dbContext.Students.Include(s => s.Depart).ToList();
            listViewStudents.ItemsSource = students;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Lọc theo Gender
            bool? filterGender = null;
            if (radioMale.IsChecked == true)
                filterGender = true;
            else if (radioFemale.IsChecked == true)
                filterGender = false;

            // Lọc theo Department
            var selectedDepartmentIds = departCheckboxes.Items
                .OfType<CheckBox>()
                .Where(cb => cb.IsChecked == true)
                .Select(cb => cb.Tag?.ToString())
                .ToList();

            // Lọc dữ liệu từ database
            var filteredStudents = _dbContext.Students
                .Include(s => s.Depart)
                .Where(student =>
                    // Filter Gender
                    (filterGender == null || student.Gender == filterGender) &&
                    // Filter Department
                    (!selectedDepartmentIds.Any() || selectedDepartmentIds.Contains(student.DepartId)))
                .ToList();

            // Cập nhật ListView
            listViewStudents.ItemsSource = filteredStudents;
        }


    }
}
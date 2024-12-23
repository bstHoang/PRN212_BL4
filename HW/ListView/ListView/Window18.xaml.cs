using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace ListView
{
    /// <summary>
    /// Interaction logic for Window18.xaml
    /// </summary>
    public partial class Window18 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window18()
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

            // Thêm CheckBox cho từng Department
            foreach (var department in departments)
            {
                var checkBox = new CheckBox
                {
                    Content = department.Name,
                    Tag = department.Id,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                stackDepartments.Children.Add(checkBox);
            }

            // Lấy danh sách Student
            var students = _dbContext.Students.Include(s => s.Depart).ToList();
            listViewStudents.ItemsSource = students;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            // Lọc theo Gender
            bool? filterGender = null;
            if (comboGender.SelectedItem is ComboBoxItem selectedGender)
            {
                var tag = selectedGender.Tag.ToString();
                if (tag == "True")
                    filterGender = true;
                else if (tag == "False")
                    filterGender = false;
            }

            // Lọc theo Department
            var selectedDepartmentIds = stackDepartments.Children
                .OfType<CheckBox>()
                .Where(cb => cb.IsChecked == true)
                .Select(cb => cb.Tag.ToString())
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

            // Thông báo nếu không có kết quả
            if (!filteredStudents.Any())
            {
                MessageBox.Show("Không tìm thấy sinh viên phù hợp với bộ lọc.");
            }
        }

    }
}

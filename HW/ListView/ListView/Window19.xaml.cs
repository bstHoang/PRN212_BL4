using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace ListView
{
    /// <summary>
    /// Interaction logic for Window19.xaml
    /// </summary>
    public partial class Window19 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window19()
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

            // Thêm tùy chọn "Tất cả" làm RadioButton đầu tiên
            var allRadio = new RadioButton
            {
                Content = "Tất cả",
                Tag = null, // Giá trị null để biểu thị không lọc
                GroupName = "DepartmentGroup", // Đảm bảo RadioButton thuộc cùng một nhóm
                IsChecked = true
            };
            stackDepartments.Children.Add(allRadio);

            // Thêm các RadioButton cho từng Department
            foreach (var department in departments)
            {
                var radioButton = new RadioButton
                {
                    Content = department.Name,
                    Tag = department.Id, // Lưu ID của Department để lọc
                    GroupName = "DepartmentGroup",
                    Margin = new Thickness(0, 5, 0, 0)
                };
                stackDepartments.Children.Add(radioButton);
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
            string? selectedDepartmentId = stackDepartments.Children
                .OfType<RadioButton>()
                .FirstOrDefault(rb => rb.IsChecked == true)?.Tag?.ToString();

            // Lọc dữ liệu từ database
            var filteredStudents = _dbContext.Students
                .Include(s => s.Depart)
                .Where(student =>
                    // Filter Gender
                    (filterGender == null || student.Gender == filterGender) &&
                    // Filter Department
                    (string.IsNullOrEmpty(selectedDepartmentId) || student.DepartId == selectedDepartmentId))
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

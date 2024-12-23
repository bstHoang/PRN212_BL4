using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window16.xaml
    /// </summary>
    public partial class Window16 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window16()
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

            // Thêm RadioButton cho "Tất cả"
            var allRadioButton = new RadioButton
            {
                Content = "Tất cả",
                GroupName = "Department",
                IsChecked = true, // Mặc định chọn "Tất cả"
                Tag = null // Tag = null để biểu thị tất cả
            };
            departRadios.Items.Add(allRadioButton);

            // Tạo RadioButton cho mỗi Department
            foreach (var department in departments)
            {
                var radioButton = new RadioButton
                {
                    Content = department.Name,
                    GroupName = "Department", // Cùng nhóm để chỉ chọn 1
                    Tag = department.Id // Lưu ID để dùng khi lọc
                };
                departRadios.Items.Add(radioButton);
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

            // Lấy Department được chọn
            var selectedDepartmentId = departRadios.Items
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

            if (!filteredStudents.Any())
            {
                MessageBox.Show("No students found matching the filters.");
            }

        }

    }
}

using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window20.xaml
    /// </summary>
    public partial class Window20 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window20()
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

            // Thêm tùy chọn "Tất cả" vào ComboBox
            comboDepartment.Items.Add(new { Id = "", Name = "Tất cả" });

            // Thêm các Department vào ComboBox
            foreach (var department in departments)
            {
                comboDepartment.Items.Add(department);
            }

            // Chọn "Tất cả" làm mặc định
            comboDepartment.SelectedIndex = 0;

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
            string? selectedDepartmentId = (comboDepartment.SelectedValue as string);

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

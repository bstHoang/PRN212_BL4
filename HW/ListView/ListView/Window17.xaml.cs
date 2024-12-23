using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window17.xaml
    /// </summary>
    public partial class Window17 : Window
    {
        private Prn212Bl14Context _dbContext;
        public Window17()
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
            comboDepartment.Items.Add(new Department { Id = null, Name = "Tất cả" });

            // Thêm các Department vào ComboBox
            foreach (var department in departments)
            {
                comboDepartment.Items.Add(department);
            }

            // Đặt tùy chọn mặc định là "Tất cả"
            comboDepartment.SelectedIndex = 0;

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
            var selectedDepartment = comboDepartment.SelectedItem as Department;
            var selectedDepartmentId = selectedDepartment?.Id;

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

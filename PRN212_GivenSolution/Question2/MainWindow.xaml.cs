using Microsoft.EntityFrameworkCore;
using Question2.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Question2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HrmanagementContext _dbContext;
        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new HrmanagementContext();
            LoadEmployee();
            LoadDepartmentsComboBox();
        }
        private void LoadEmployee()
        {
            using (var context = new HrmanagementContext())
            {
                var studentList = context.Employees.Include(x => x.Position).Include(y => y.Department).ToList();
                dataGridEmployee.ItemsSource = studentList;
                cbFilterDepartment.ItemsSource = context.Departments.ToList();
                cbPositionId11.ItemsSource = context.Positions.ToList();

            }
        }
        private void LoadDepartmentsComboBox()
        {
            using (var context = new HrmanagementContext())
            {
                var departments = context.Departments.ToList();

                // Thêm tùy chọn "All" vào danh sách
                departments.Insert(0, new Department { DepartmentId = 0, DepartmentName = "All Departments" });
                cbFilterDepartment.ItemsSource = departments;

                cbFilterDepartment.SelectedIndex = 0; // Mặc định chọn "All"
            }
        }
        private void dataGridEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridEmployee.SelectedItem is Employee selectedStudent)
            {
                txtName.Text = selectedStudent.FullName;
                cbPositionId11.SelectedValue = selectedStudent.PositionId; // Đặt giá trị theo Id
                dpkDOB.SelectedDate = selectedStudent.DateOfBirth.ToDateTime(TimeOnly.MinValue); // Chuyển từ DateOnly sang DateTime
                dpkdoh.SelectedDate = selectedStudent.DateOfHire.ToDateTime(TimeOnly.MinValue);
            }
        }
        private void cbFilterDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new HrmanagementContext())
            {
                var selectedDepartment = cbFilterDepartment.SelectedItem as Department;

                // Nếu chọn "All", hiển thị toàn bộ sinh viên
                var filteredStudents = selectedDepartment?.DepartmentId == 0
                    ? context.Employees.Include(x => x.Department).ToList()
                    : context.Employees.Include(x => x.Department).Where(x => x.DepartmentId == selectedDepartment.DepartmentId).ToList();

                dataGridEmployee.ItemsSource = filteredStudents;
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HrmanagementContext())
            {
                var newEployee = new Employee
                {
                    FullName = txtName.Text,
                    DateOfHire = DateOnly.FromDateTime(dpkdoh.SelectedDate ?? DateTime.Now),
                    DateOfBirth = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now),
                    //PositionId = cbPositionId.SelectedValue,
                }; 

                //context.Employees.Add(Employee);
                context.SaveChanges();
                LoadEmployee();
            }
        }
    }
}
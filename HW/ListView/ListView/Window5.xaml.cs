using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
            LoadStudent();
            LoadDepartmentsRadio();
        }

        private void LoadStudent()
        {
            using (var context = new Prn212Bl14Context())
            {
                var studentList = context.Students.Include(x => x.Depart).ToList();
                listViewStudents.ItemsSource = studentList; // Sử dụng ListView thay vì DataGrid
            }
        }

        private void LoadDepartmentsRadio()
        {
            using (var context = new Prn212Bl14Context())
            {
                var departments = context.Departments.ToList();

                // Xóa tất cả các RadioButton trước đó (nếu có)
                stackPanelDepartments.Children.Clear();

                // Tạo một RadioButton cho "All" để hiển thị tất cả sinh viên
                var allRadioButton = new RadioButton
                {
                    Content = "All",
                    Tag = "All", // Gắn tag để nhận diện
                    GroupName = "Departments" // Đảm bảo các RadioButton nằm trong cùng một nhóm
                };
                allRadioButton.Checked += DepartmentRadioButton_Checked;
                stackPanelDepartments.Children.Add(allRadioButton);

                // Tạo các RadioButton cho từng Department
                foreach (var department in departments)
                {
                    var radioButton = new RadioButton
                    {
                        Content = department.Name,
                        Tag = department.Id, // Lưu Id của Department vào Tag
                        GroupName = "Departments" // Đảm bảo các RadioButton nằm trong cùng một nhóm
                    };
                    radioButton.Checked += DepartmentRadioButton_Checked;
                    stackPanelDepartments.Children.Add(radioButton);
                }

                // Mặc định chọn "All"
                allRadioButton.IsChecked = true;
            }
        }

        private void DepartmentRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton selectedRadioButton)
            {
                using (var context = new Prn212Bl14Context())
                {
                    var selectedDepartmentId = selectedRadioButton.Tag.ToString();

                    // Nếu chọn "All", hiển thị tất cả sinh viên
                    var filteredStudents = selectedDepartmentId == "All"
                        ? context.Students.Include(x => x.Depart).ToList()
                        : context.Students.Include(x => x.Depart).Where(x => x.DepartId == selectedDepartmentId).ToList();

                    listViewStudents.ItemsSource = filteredStudents;
                }
            }
        }
    }
}

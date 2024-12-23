using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace ListView
{
    /// <summary>
    /// Interaction logic for Window6.xaml
    /// </summary>
    public partial class Window6 : Window
    {
        public Window6()
        {
            InitializeComponent();
            LoadStudent();
            LoadDepartmentsCheckBox();
        }

        private void LoadStudent()
        {
            using (var context = new Prn212Bl14Context())
            {
                var studentList = context.Students.Include(x => x.Depart).ToList();
                listViewStudents.ItemsSource = studentList; // Sử dụng ListView thay vì DataGrid
            }
        }

        private void LoadDepartmentsCheckBox()
        {
            using (var context = new Prn212Bl14Context())
            {
                var departments = context.Departments.ToList();

                // Xóa tất cả các CheckBox trước đó (nếu có)
                stackPanelDepartments.Children.Clear();

                // Tạo một CheckBox cho "All" để hiển thị tất cả sinh viên
                var allCheckBox = new CheckBox
                {
                    Content = "All",
                    Tag = "All", // Gắn tag để nhận diện
                    IsChecked = true // Mặc định chọn "All"
                };
                allCheckBox.Checked += DepartmentCheckBox_Checked;
                allCheckBox.Unchecked += DepartmentCheckBox_Unchecked;
                stackPanelDepartments.Children.Add(allCheckBox);

                // Tạo các CheckBox cho từng Department
                foreach (var department in departments)
                {
                    var checkBox = new CheckBox
                    {
                        Content = department.Name,
                        Tag = department.Id // Lưu Id của Department vào Tag
                    };
                    checkBox.Checked += DepartmentCheckBox_Checked;
                    checkBox.Unchecked += DepartmentCheckBox_Unchecked;
                    stackPanelDepartments.Children.Add(checkBox);
                }
            }
        }

        private void DepartmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox selectedCheckBox)
            {
                // Uncheck tất cả các CheckBox khác
                foreach (var child in stackPanelDepartments.Children)
                {
                    if (child is CheckBox checkBox && checkBox != selectedCheckBox)
                    {
                        checkBox.IsChecked = false;
                    }
                }

                // Lấy ID của Department đã chọn
                var selectedDepartmentId = selectedCheckBox.Tag.ToString();

                using (var context = new Prn212Bl14Context())
                {
                    // Nếu chọn "All", hiển thị tất cả sinh viên
                    var filteredStudents = selectedDepartmentId == "All"
                        ? context.Students.Include(x => x.Depart).ToList()
                        : context.Students.Include(x => x.Depart).Where(x => x.DepartId == selectedDepartmentId).ToList();

                    listViewStudents.ItemsSource = filteredStudents;
                }
            }
        }

        private void DepartmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Đảm bảo ít nhất một CheckBox luôn được chọn (có thể thêm logic nếu cần)
            var isAnyChecked = false;
            foreach (var child in stackPanelDepartments.Children)
            {
                if (child is CheckBox checkBox && checkBox.IsChecked == true)
                {
                    isAnyChecked = true;
                    break;
                }
            }

            if (!isAnyChecked)
            {
                // Mặc định chọn "All" nếu không có CheckBox nào được chọn
                if (stackPanelDepartments.Children[0] is CheckBox allCheckBox)
                {
                    allCheckBox.IsChecked = true;
                }
            }
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Slot_11_12.Models;
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

namespace Slot_11_12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadStudent();
            LoadDepartmentsComboBox();
            LoadDepartmentsRadio();
            LoadDepartmentsCheckBox();
        }
        private void LoadStudent()
        {
            using (var context = new Prn212Bl14Context())
            {
                var studentList = context.Students.Include(x => x.Depart).ToList();
                listViewStudents.ItemsSource = studentList; // Sử dụng ListView thay vì DataGrid
                cbDerpartmentID.ItemsSource = context.Departments.ToList();
            }
        }
        //---------------- combobox department Start----------------------------
        private void LoadDepartmentsComboBox()
        {
            using (var context = new Prn212Bl14Context())
            {
                var departments = context.Departments.ToList();

                // Thêm tùy chọn "All" vào danh sách
                departments.Insert(0, new Department { Id = "All", Name = "All Departments" });
                cbFilterDepartment.ItemsSource = departments;

                cbFilterDepartment.SelectedIndex = 0; // Mặc định chọn "All"
            }
        }


        private void cbFilterDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new Prn212Bl14Context())
            {
                var selectedDepartment = cbFilterDepartment.SelectedItem as Department;

                // Nếu chọn "All", hiển thị toàn bộ sinh viên
                var filteredStudents = selectedDepartment?.Id == "All"
                    ? context.Students.Include(x => x.Depart).ToList()
                    : context.Students.Include(x => x.Depart).Where(x => x.DepartId == selectedDepartment.Id).ToList();

                listViewStudents.ItemsSource = filteredStudents;
            }
        }
        //---------------- combobox department End----------------------------
        //---------------- radio department Start----------------------------
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
        //---------------- radio department End----------------------------
        //---------------- checkbox department Start----------------------------
        private void LoadDepartmentsCheckBox()
        {
            using (var context = new Prn212Bl14Context())
            {
                var departments = context.Departments.ToList();

                // Xóa tất cả các CheckBox trước đó (nếu có)
                stackPanelDepartments3.Children.Clear();

                // Tạo CheckBox cho "All" để hiển thị tất cả sinh viên
                var allCheckBox = new CheckBox
                {
                    Content = "All",
                    Tag = "All"
                };
                allCheckBox.Checked += AllCheckBox_Checked;
                allCheckBox.Unchecked += AllCheckBox_Unchecked;
                stackPanelDepartments3.Children.Add(allCheckBox);

                // Tạo CheckBox cho từng Department
                foreach (var department in departments)
                {
                    var checkBox = new CheckBox
                    {
                        Content = department.Name,
                        Tag = department.Id
                    };
                    checkBox.Checked += DepartmentCheckBox_Checked;
                    checkBox.Unchecked += DepartmentCheckBox_Unchecked;
                    stackPanelDepartments3.Children.Add(checkBox);
                }
            }
        }
        private void AllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var child in stackPanelDepartments3.Children)
            {
                if (child is CheckBox checkBox && (string)checkBox.Tag != "All")
                {
                    checkBox.IsChecked = true;
                }
            }
        }

        private void AllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var child in stackPanelDepartments3.Children)
            {
                if (child is CheckBox checkBox && (string)checkBox.Tag != "All")
                {
                    checkBox.IsChecked = false;
                }
            }
        }

        private void DepartmentCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // Nếu tất cả CheckBox (trừ "All") được chọn, thì đánh dấu "All"
            if (stackPanelDepartments3.Children.OfType<CheckBox>()
                .Where(cb => (string)cb.Tag != "All")
                .All(cb => cb.IsChecked == true))
            {
                var allCheckBox = stackPanelDepartments3.Children.OfType<CheckBox>()
                    .FirstOrDefault(cb => (string)cb.Tag == "All");
                if (allCheckBox != null)
                    allCheckBox.IsChecked = true;
            }
        }

        private void DepartmentCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Nếu bất kỳ CheckBox nào bị bỏ chọn, thì bỏ chọn "All"
            var allCheckBox = stackPanelDepartments3.Children.OfType<CheckBox>()
                .FirstOrDefault(cb => (string)cb.Tag == "All");
            if (allCheckBox != null)
                allCheckBox.IsChecked = false;
        }
        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            var selectedDepartments = stackPanelDepartments3.Children.OfType<CheckBox>()
                .Where(cb => cb.IsChecked == true && (string)cb.Tag != "All")
                .Select(cb => cb.Tag.ToString())
                .ToList();

            using (var context = new Prn212Bl14Context())
            {
                var filteredStudents = selectedDepartments.Count == 0
                    ? context.Students.Include(x => x.Depart).ToList() // Nếu không chọn gì, hiển thị tất cả
                    : context.Students.Include(x => x.Depart)
                        .Where(x => selectedDepartments.Contains(x.DepartId)).ToList();

                listViewStudents.ItemsSource = filteredStudents;
            }
        }
        //---------------- checkbox department End----------------------------
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Prn212Bl14Context())
            {
                var selectedGender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString(); // Lấy giá trị được chọn từ ComboBox
                var newStudent = new Student
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    //Gender = chkGender.IsChecked ?? false,
                    //Gender = rbtnMale.IsChecked == true,
                    Gender = selectedGender == "Male",
                    DepartId = cbDerpartmentID.SelectedValue.ToString(),
                    Dob = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now),
                    Gpa = double.Parse(txtGPA.Text)
                };

                context.Students.Add(newStudent);
                context.SaveChanges();
                LoadStudent(); // Refresh DataGrid
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStudents.SelectedItem is Student selectedStudent)
            {
                using (var context = new Prn212Bl14Context())
                {
                    var student = context.Students.Find(selectedStudent.Id);
                    if (student != null)
                    {
                        student.Name = txtName.Text;
                        //student.Gender = chkGender.IsChecked ?? false;
                        //student.Gender = rbtnMale.IsChecked == true;
                        var selectedGender = (cbGender.SelectedItem as ComboBoxItem)?.Content.ToString(); // Lấy giá trị được chọn từ ComboBox
                        student.Gender = selectedGender == "Male";
                        student.DepartId = cbDerpartmentID.SelectedValue.ToString();
                        student.Dob = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now);
                        student.Gpa = double.Parse(txtGPA.Text);

                        context.SaveChanges();
                        LoadStudent(); // Refresh DataGrid

                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listViewStudents.SelectedItem is Student selectedStudent)
            {
                using (var context = new Prn212Bl14Context())
                {
                    var student = context.Students.Find(selectedStudent.Id);
                    if (student != null)
                    {
                        context.Students.Remove(student);
                        context.SaveChanges();
                        LoadStudent(); // Refresh DataGrid
                    }
                }
            }

        }
        private void listViewStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewStudents.SelectedItem is Student selectedStudent)
            {
                // Điền thông tin của sinh viên được chọn vào các trường nhập liệu
                txtId.Text = selectedStudent.Id.ToString();
                txtName.Text = selectedStudent.Name;
                //chkGender.IsChecked = selectedStudent.Gender;
                //rbtnMale.IsChecked = selectedStudent.Gender;
                //rbtnFemale.IsChecked = !selectedStudent.Gender;
                cbGender.SelectedItem = cbGender.Items
            .Cast<ComboBoxItem>()
            .FirstOrDefault(item => item.Content.ToString() == (selectedStudent.Gender ? "Male" : "Female"));
                cbDerpartmentID.SelectedValue = selectedStudent.DepartId; // Đặt giá trị theo Id
                dpkDOB.SelectedDate = selectedStudent.Dob?.ToDateTime(TimeOnly.MinValue); // Chuyển từ DateOnly sang DateTime
                txtGPA.Text = selectedStudent.Gpa.ToString();
            }
        }

    }
}
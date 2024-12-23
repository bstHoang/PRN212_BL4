using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window11.xaml
    /// </summary>
    public partial class Window11 : Window
    {
        public Window11()
        {
            InitializeComponent();
            LoadStudent();
        }

        private void LoadStudent()
        {
            using (var context = new Prn212Bl14Context())
            {
                var studentList = context.Students.Include(x => x.Depart).ToList();
                listViewStudents.ItemsSource = studentList;

                // Load danh sách Department vào CheckBox group
                var departments = context.Departments.ToList();
                checkBoxGroup.ItemsSource = departments; ; // Hiển thị tên phòng ban
            }
        }
        private string selectedDepartmentId; // Lưu trữ Department ID được chọn

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                selectedDepartmentId = checkBox.Tag.ToString();

                // Bỏ chọn tất cả CheckBox khác
                foreach (var item in checkBoxGroup.Items)
                {
                    var container = (CheckBox)checkBoxGroup.ItemContainerGenerator.ContainerFromItem(item);
                    if (container != null && container != checkBox)
                    {
                        container.IsChecked = false;
                    }
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.Tag.ToString() == selectedDepartmentId)
            {
                selectedDepartmentId = null; // Nếu CheckBox được bỏ chọn, xóa giá trị đã lưu
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Prn212Bl14Context())
            {
                var newStudent = new Student
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    Gender = (cbGender.SelectedItem as ComboBoxItem)?.Tag?.ToString() == "True",
                    DepartId = selectedDepartmentId, // Lấy DepartmentId đầu tiên
                    Dob = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now),
                    Gpa = double.Parse(txtGPA.Text)
                };

                context.Students.Add(newStudent);
                context.SaveChanges();
                LoadStudent();
                ClearInputs();
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
                        student.Gender = (cbGender.SelectedItem as ComboBoxItem)?.Tag?.ToString() == "True";
                        student.DepartId = selectedDepartmentId; // Lấy DepartmentId đầu tiên
                        student.Dob = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now);
                        student.Gpa = double.Parse(txtGPA.Text);

                        context.SaveChanges();
                        LoadStudent();
                        ClearInputs();
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
                        LoadStudent();
                        ClearInputs();
                    }
                }
            }
        }

        private void listViewStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewStudents.SelectedItem is Student selectedStudent)
            {
                txtId.Text = selectedStudent.Id.ToString();
                txtName.Text = selectedStudent.Name;
                cbGender.SelectedItem = cbGender.Items
                    .Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Tag?.ToString() == selectedStudent.Gender.ToString());
                dpkDOB.SelectedDate = selectedStudent.Dob?.ToDateTime(TimeOnly.MinValue);
                txtGPA.Text = selectedStudent.Gpa.ToString();

                // Đánh dấu các CheckBox tương ứng với DepartmentId của sinh viên
                foreach (var item in checkBoxGroup.Items)
                {
                    var container = (CheckBox)checkBoxGroup.ItemContainerGenerator.ContainerFromItem(item);
                    if (container != null)
                    {
                        container.IsChecked = container.Tag?.ToString() == selectedStudent.DepartId;
                    }
                }
            }
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtName.Clear();
            cbGender.SelectedIndex = -1;
            dpkDOB.SelectedDate = null;
            txtGPA.Clear();

            // Bỏ chọn tất cả các CheckBox
            foreach (var item in checkBoxGroup.Items)
            {
                var container = (CheckBox)checkBoxGroup.ItemContainerGenerator.ContainerFromItem(item);
                if (container != null)
                {
                    container.IsChecked = false;
                }
            }

            selectedDepartmentId = null; // Xóa danh sách đã chọn
        }

    }
}
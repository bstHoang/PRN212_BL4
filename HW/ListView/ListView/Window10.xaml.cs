using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window10.xaml
    /// </summary>
    public partial class Window10 : Window
    {
        public Window10()
        {
            InitializeComponent();
            LoadStudent();
        }
        private void LoadStudent()
        {
            using (var context = new Prn212Bl14Context())
            {
                var studentList = context.Students.Include(x => x.Depart).ToList();
                listViewStudents.ItemsSource = studentList; // Sử dụng ListView thay vì DataGrid

                spDepartments.Children.Clear(); // Xóa các RadioButton cũ
                foreach (var department in context.Departments.ToList())
                {
                    var radioButton = new RadioButton
                    {
                        Content = department.Name,
                        Tag = department.Id, // Sử dụng Tag để lưu Id của Department
                        GroupName = "DepartmentGroup"
                    };
                    spDepartments.Children.Add(radioButton);
                }
            }
        }
        
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new Prn212Bl14Context())
            {
                var selectedDepartment = spDepartments.Children
                    .OfType<RadioButton>()
                    .FirstOrDefault(rb => rb.IsChecked == true);
                if (selectedDepartment == null)
                {
                    MessageBox.Show("Please select a department.");
                    return;
                }

                var newStudent = new Student
                {
                    Id = int.Parse(txtId.Text),
                    Name = txtName.Text,
                    Gender = (cbGender.SelectedItem as ComboBoxItem)?.Tag?.ToString() == "True",
                    DepartId = selectedDepartment.Tag.ToString(),
                    Dob = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now),
                    Gpa = double.Parse(txtGPA.Text)
                };

                context.Students.Add(newStudent);
                context.SaveChanges();
                LoadStudent(); // Refresh DataGrid
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
                        var selectedDepartment = spDepartments.Children
                            .OfType<RadioButton>()
                            .FirstOrDefault(rb => rb.IsChecked == true);

                        if (selectedDepartment == null)
                        {
                            MessageBox.Show("Please select a department.");
                            return;
                        }
                        student.Name = txtName.Text;
                        student.Gender = (cbGender.SelectedItem as ComboBoxItem)?.Tag?.ToString() == "True";
                        student.DepartId = selectedDepartment.Tag.ToString();
                        student.Dob = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now);
                        student.Gpa = double.Parse(txtGPA.Text);

                        context.SaveChanges();
                        LoadStudent(); // Refresh DataGrid
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
                        LoadStudent(); // Refresh DataGrid
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

                foreach (RadioButton rb in spDepartments.Children)
                {
                    rb.IsChecked = rb.Tag?.ToString() == selectedStudent.DepartId;
                }
            }
        }

        private void ClearInputs()
        {
            txtId.Clear(); // Xóa nội dung TextBox
            txtName.Clear();
            cbGender.SelectedIndex = -1;
            foreach (RadioButton rb in spDepartments.Children)
            {
                rb.IsChecked = false;
            }
            dpkDOB.SelectedDate = null; // Xóa ngày trong DatePicker
            txtGPA.Clear(); // Xóa nội dung TextBox
        }

    }
}
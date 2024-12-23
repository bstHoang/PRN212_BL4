using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
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
                cbDerpartmentID.ItemsSource = context.Departments.ToList();
                cbDerpartmentID.DisplayMemberPath = "Name";
                cbDerpartmentID.SelectedValuePath = "Id";
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
                    Gender = rdoMale.IsChecked == true,
                    DepartId = cbDerpartmentID.SelectedValue.ToString(),
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
                        student.Name = txtName.Text;
                        student.Gender = rdoMale.IsChecked == true;
                        student.DepartId = cbDerpartmentID.SelectedValue.ToString();
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
                // Điền thông tin của sinh viên được chọn vào các trường nhập liệu
                txtId.Text = selectedStudent.Id.ToString();
                txtName.Text = selectedStudent.Name;
                rdoMale.IsChecked = selectedStudent.Gender; // True nếu "Nam"
                rdoFemale.IsChecked = !selectedStudent.Gender; // False nếu "Nữ"
                cbDerpartmentID.SelectedValue = selectedStudent.DepartId; // Đặt giá trị theo Id
                dpkDOB.SelectedDate = selectedStudent.Dob?.ToDateTime(TimeOnly.MinValue); // Chuyển từ DateOnly sang DateTime
                txtGPA.Text = selectedStudent.Gpa.ToString();
            }
        }
        private void ClearInputs()
        {
            txtId.Clear(); // Xóa nội dung TextBox
            txtName.Clear();
            rdoMale.IsChecked = false; // Bỏ chọn RadioButton "Nam"
            rdoFemale.IsChecked = false; // Bỏ chọn RadioButton "Nữ"
            cbDerpartmentID.SelectedIndex = -1; // Đặt ComboBox về trạng thái không chọn
            dpkDOB.SelectedDate = null; // Xóa ngày trong DatePicker
            txtGPA.Clear(); // Xóa nội dung TextBox
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using WpfApp2.Models;

namespace WpfApp2
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
                    Gender = chkGender.IsChecked ?? false,
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
                        student.Gender = chkGender.IsChecked ?? false;
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
                chkGender.IsChecked = selectedStudent.Gender;
                cbDerpartmentID.SelectedValue = selectedStudent.DepartId; // Đặt giá trị theo Id
                dpkDOB.SelectedDate = selectedStudent.Dob?.ToDateTime(TimeOnly.MinValue); // Chuyển từ DateOnly sang DateTime
                txtGPA.Text = selectedStudent.Gpa.ToString();
            }
        }

    }
}
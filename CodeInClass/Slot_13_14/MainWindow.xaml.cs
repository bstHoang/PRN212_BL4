using System.Windows;
using System.Windows.Controls;
using Slot_13_14.Models;


namespace Slot_13_14
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
                dataGridStudents.ItemsSource = studentList;
                cbDerpartmentID.ItemsSource = context.Departments.ToList();
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
            if (dataGridStudents.SelectedItem is Student selectedStudent)
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
            if (dataGridStudents.SelectedItem is Student selectedStudent)
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

        private void dataGridStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (dataGridStudents.SelectedItem is Student selectedProject)
            //{
            //    txtId.Text = selectedProject.Id.ToString();
            //    txtName.Text = selectedProject.Name;
            //    chkGender.Text = selectedProject.Description;
            //    datePickerStartDate.SelectedDate = selectedProject.StartDate?.ToDateTime(TimeOnly.MinValue); // Chuyển từ DateOnly sang DateTime
            //    comboBoxType.Text = selectedProject.Type; // Cập nhật loại dự án
            //}
        }

        private void cbFilterDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
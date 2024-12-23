using ListView.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace ListView
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
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID phải là số nguyên hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new Prn212Bl14Context())
            {
                var existingStudent = context.Students.AsNoTracking().FirstOrDefault(s => s.Id == id);
                if (existingStudent != null)
                {
                    MessageBox.Show("ID đã tồn tại, vui lòng chọn ID khác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

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
                ClearInputs();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                cbDerpartmentID.SelectedValue == null ||
                !dpkDOB.SelectedDate.HasValue ||
                string.IsNullOrWhiteSpace(txtGPA.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("ID phải là số nguyên hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(txtGPA.Text, out double gpa))
            {
                MessageBox.Show("GPA phải là số hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (listViewStudents.SelectedItem is Student selectedStudent)
            {
                using (var context = new Prn212Bl14Context())
                {
                    var existingStudent = context.Students.AsNoTracking().FirstOrDefault(s => s.Id == id);

                    // Kiểm tra nếu ID đã tồn tại nhưng không phải của sinh viên đang được cập nhật
                    if (existingStudent != null && existingStudent.Id != selectedStudent.Id)
                    {
                        MessageBox.Show("ID k dc thay doi", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var student = context.Students.Find(selectedStudent.Id);
                    if (student != null)
                    {
                        student.Id = id;
                        student.Name = txtName.Text;
                        student.Gender = chkGender.IsChecked ?? false;
                        student.DepartId = cbDerpartmentID.SelectedValue.ToString();
                        student.Dob = DateOnly.FromDateTime(dpkDOB.SelectedDate ?? DateTime.Now);
                        student.Gpa = gpa;

                        context.SaveChanges();
                        LoadStudent(); // Refresh ListView
                        ClearInputs();

                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
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
                chkGender.IsChecked = selectedStudent.Gender;
                cbDerpartmentID.SelectedValue = selectedStudent.DepartId; // Đặt giá trị theo Id
                dpkDOB.SelectedDate = selectedStudent.Dob?.ToDateTime(TimeOnly.MinValue); // Chuyển từ DateOnly sang DateTime
                txtGPA.Text = selectedStudent.Gpa.ToString();
            }
        }
        private void ClearInputs()
        {
            txtId.Clear(); // Xóa nội dung TextBox
            txtName.Clear(); // Xóa nội dung TextBox
            chkGender.IsChecked = null; // Đặt CheckBox về trạng thái không chọn (hoặc false nếu mặc định là không)
            cbDerpartmentID.SelectedIndex = -1; // Đặt ComboBox về trạng thái không chọn
            dpkDOB.SelectedDate = null; // Xóa ngày trong DatePicker
            txtGPA.Clear(); // Xóa nội dung TextBox
        }

    }
}
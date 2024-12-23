using Project_PRN212_FALL24.Models;
using System.Windows;
using System.Windows.Input;

namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for ManageRole.xaml
    /// </summary>
    public partial class ManageRole : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public ManageRole(string email)
        {
            InitializeComponent();
            LoadRole();
            EmailTextBlock.Text = email;
            AdminAccountTextBlock.Visibility = Visibility.Collapsed;
        }

        private void LoadRole() {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                var role = db.Settings.Where(x => x.Type == 1).ToList();
                RoleDataGrid.ItemsSource = role;   
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát không?",
                                              "Xác nhận thoát",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
        private void Popup_Click(object sender, MouseButtonEventArgs e)
        {
            ProfilePopup.IsOpen = !ProfilePopup.IsOpen;
        }

        private void Profile_Click(object sender, MouseButtonEventArgs e)
        {
            ProfilePopup.IsOpen = true;
        }

        private void AccountInfo_Click(object sender, MouseButtonEventArgs e)
        {
            UserInfo mainwindown = new UserInfo(EmailTextBlock.Text);
            mainwindown.Show();
            this.Close();
        }

        private void BorrowHistory_Click(object sender, MouseButtonEventArgs e)
        {
            UserHistory userHistory = new UserHistory(EmailTextBlock.Text);
            userHistory.Show();
            this.Close();
        }

        private void AdminAccount_Click(object sender, MouseButtonEventArgs e)
        {
            AdminHomePage adminHome = new AdminHomePage(EmailTextBlock.Text);
            adminHome.Show();
            this.Close();
        }

        private void Logout_Click(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            this.Close();
            login.Show();
        }
        private void Logo_Click(object sender, MouseButtonEventArgs e)
        {
            HomePage mainWindow = new HomePage(EmailTextBlock.Text, 1);
            mainWindow.Show();
            this.Close();
        }

        private void AdminAccount_Click(object sender, RoutedEventArgs e)
        {
            AdminHomePage mainWindow = new AdminHomePage(EmailTextBlock.Text);
            mainWindow.Show();
            this.Close();
        }

        private void ManageBook_Click(object sender, RoutedEventArgs e)
        {
            ManageBook mainwindow = new ManageBook(EmailTextBlock.Text);
            mainwindow.Show();
            this.Close();

        }

        private void ManageBookCategory_Click(object sender, RoutedEventArgs e)
        {
            ManageTypeBook mainwindow = new ManageTypeBook(EmailTextBlock.Text);
            mainwindow.Show();
            this.Close();
        }

        private void ManageUsers_Click(object sender, RoutedEventArgs e)
        {
            ManageUser mainwindow = new ManageUser(EmailTextBlock.Text);
            mainwindow.Show();
            this.Close();
        }

        private void ManageRole_Click(object sender, RoutedEventArgs e)
        {
            ManageRole role = new ManageRole(EmailTextBlock.Text);
            role.Show();
            this.Close();
        }

        private void RoleDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RoleDataGrid.SelectedItem is Setting selectedSetting) {
                txtID.Text = selectedSetting.Id.ToString();
                txtName.Text = selectedSetting.Name;
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if ( string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return; 
            }

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var existRole = db.Settings.FirstOrDefault(x => x.Name.Equals(txtName.Text));

                if (existRole == null) 
                {
                    var setting = new Setting
                    {
                        Type = 1,
                        Name = txtName.Text
                    };

                    db.Settings.Add(setting);
                    db.SaveChanges();
                    MessageBox.Show("Thêm vai trò mới thành công");
                    ClearInputs();
                    LoadRole();
                }
                else 
                {
                    MessageBox.Show("Tên vai trò này đã tồn tại rồi");
                }
            }
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một loại sách để cập nhật.");
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                var roleId = int.Parse(txtID.Text);
                var roleName = db.Settings.FirstOrDefault(x => x.Id == roleId);

                var existRole = db.Settings.FirstOrDefault(x => x.Name.Equals(txtName.Text));
                if (existRole != null) {
                    MessageBox.Show("Tên vai trò này đã tồn tại rồi");
                }

                else if (roleName != null)
                {
                    roleName.Name = txtName.Text;
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sửa vai trò thành công");
                    LoadRole();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại vai trò cần cập nhật.");
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một loại vai trò để xoá.");
                return;
            }
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa vai trò này?",
                                        "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                 using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
                 {
                    var roleId = int.Parse(txtID.Text);
                    var roleToDelete = db.Settings.FirstOrDefault(x => x.Id == roleId);

                    if (roleToDelete != null)
                    {
                        db.Settings.Remove(roleToDelete);
                        db.SaveChanges();
                        MessageBox.Show("Xóa loại vai trò thành công!");
                        LoadRole();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy loại vai trò để xóa.");
                    }
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        { 
            txtID.Clear();
            txtName.Clear();
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var query = db.Settings.Where(x => x.Type == 1).AsQueryable();

                if (!string.IsNullOrEmpty(TextBoxSearch.Text))
                {
                    query = query.Where(b => b.Name.Contains(TextBoxSearch.Text));
                }
                var result = query.ToList();
                RoleDataGrid.ItemsSource = result;
            }
        }

        private void ManageNookLoan_Click(object sender, RoutedEventArgs e)
        {
            BookHistory bookHistory = new BookHistory(EmailTextBlock.Text);
            bookHistory.Show();
            this.Close();
        }
    }
}

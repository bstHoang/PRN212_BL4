using Project_PRN212_FALL24.Models;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for ManageUser.xaml
    /// </summary>
    public partial class ManageUser : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public ManageUser(string email)
        {
            InitializeComponent();
            loadUser();
            loadComboboxRole();
            loadComboBoxRoleFilter();
            EmailTextBlock.Text = email;
            AdminAccountTextBlock.Visibility = Visibility.Collapsed;
        }
        private void loadUser() {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                var acc = db.Accounts.Include(x => x.RoleNavigation).ToList();
                UserDataGrid.ItemsSource = acc;
            }
        }

        private void loadComboboxRole() {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var role = db.Settings.Where(x => x.Type == 1).ToList();
                cbbRole.ItemsSource = role;
            }
        }

        private void loadComboBoxRoleFilter(){
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                var role = db.Settings.Where(x => x.Type == 1).ToList();
                var allRole = new Setting { Id = 0, Name = "All" };
                role.Insert(0, allRole);
                ComboBoxRoleFilter.ItemsSource = role;
                ComboBoxRoleFilter.SelectedIndex = 0;
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

        private void UserDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UserDataGrid.SelectedItem is Account selectedAcc) {
                txtID.Text = selectedAcc.Id.ToString();
                txtEmail.Text = selectedAcc.Email;
                txtPassword.Password = selectedAcc.Password;
                cbbRole.SelectedValue = selectedAcc.Role.ToString();
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Password)
                || cbbRole.SelectedValue == null) {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            var acc = new Account { 
                Email = txtEmail.Text,  
                Password = txtPassword.Password,
                Role = Convert.ToInt32(cbbRole.SelectedValue)
            };
            
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                var existEmail = db.Accounts.FirstOrDefault(x => x.Email.Equals(txtEmail.Text));
                if (existEmail == null)
                {
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    MessageBox.Show("Thêm người dùng thành công!");
                    loadUser();
                    ClearInput();
                }
                else {
                    MessageBox.Show("Người dùng đã tồn tại");
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một người dùng để cập nhật.");
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPassword.Password)
                || cbbRole.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) { 
                var userID = int.Parse(txtID.Text);
                var userIdToUpdate = db.Accounts.FirstOrDefault(x => x.Id == userID);

                if (!userIdToUpdate.Email.Equals(txtEmail.Text) 
                    || !userIdToUpdate.Password.Equals(txtPassword.Password)) 
                {
                    MessageBox.Show("Không được thay đổi thông tin của người dùng");
                }
                else if (userIdToUpdate != null) { 
                    userIdToUpdate.Role = Convert.ToInt32(cbbRole.SelectedValue);

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật thành công");
                    ClearInput();
                    loadUser();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?",
                                        "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                    var userId = int.Parse(txtID.Text);
                    var userToDelete = db.Accounts.FirstOrDefault(b => b.Id == userId);
                    if (userToDelete != null)
                    {
                        db.Accounts.Remove(userToDelete);
                        db.SaveChanges();
                        MessageBox.Show("Xóa sách thành công!");
                        loadUser();
                        ClearInput();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy người để xóa.");
                    }
                }
            }
        }

        private void ClearInput() { 
            txtID.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cbbRole.SelectedValue = -1;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) 
            {
                var query = db.Accounts.Include(x => x.RoleNavigation).AsQueryable();

                if (!string.IsNullOrEmpty(TextBoxSearch.Text))
                {
                    query = query.Where(b => b.Email.Contains(TextBoxSearch.Text));
                }
                if (ComboBoxRoleFilter.SelectedIndex > 0)
                {
                    int selectedType = (int)ComboBoxRoleFilter.SelectedValue;
                    query = query.Where(b => b.Role == selectedType);
                }
                var result = query.ToList();

                UserDataGrid.ItemsSource = result;
            }
        }

        private void ManageRole_Click(object sender, RoutedEventArgs e)
        {
            ManageRole role = new ManageRole(EmailTextBlock.Text);
            role.Show();
            this.Close();
        }

        private void ManageNookLoan_Click(object sender, RoutedEventArgs e)
        {
            BookHistory bookHistory = new BookHistory(EmailTextBlock.Text);
            bookHistory.Show();
            this.Close();
        }
    }
}
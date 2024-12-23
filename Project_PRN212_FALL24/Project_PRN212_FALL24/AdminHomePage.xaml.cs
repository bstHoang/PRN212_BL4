using Project_PRN212_FALL24.Models;
using Project_PRN212_FALL24.Validate;
using System.Windows;
using System.Windows.Input;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for AdminHomePage.xaml
    /// </summary>
    public partial class AdminHomePage : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public AdminHomePage(string email)
        {
            InitializeComponent();
            EmailTextBlock.Text = email;
            AdminAccountTextBlock.Visibility = Visibility.Collapsed;
            txtEmail.Text = email;
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
            HomePage mainWindow = new HomePage(EmailTextBlock.Text,1);
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            ValidateAll validate = new ValidateAll();
            string email = txtEmail.Text.Trim();
            string oldPassword = txtOldPassword.Password.Trim();
            string newPassword = txtNewPassword.Password.Trim();
            string confirmPassword = txtCFNewPassword.Password.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(oldPassword) ||
                string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword)) 
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", 
                    MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            var acc = db.Accounts.FirstOrDefault(x => x.Email == email);

            if (acc.Password != oldPassword) {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Lỗi", MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            if (newPassword != confirmPassword) {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu mới không trùng khớp", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!validate.ValidatePassWord(newPassword)) {
                MessageBox.Show("Mật khẩu không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            acc.Password = newPassword;
            db.SaveChanges();
            MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtCFNewPassword.Clear();
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
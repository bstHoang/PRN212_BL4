using Microsoft.VisualBasic.ApplicationServices;
using Project_PRN212_FALL24.HandleGmail;
using Project_PRN212_FALL24.Models;
using System.Windows;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public Login()
        {
            InitializeComponent();
        }
        // code-behind xu ly nut dang nhap
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var acc = db.Accounts.FirstOrDefault(checkacc => checkacc.Email == EmailTextBox.Text);
            if (string.IsNullOrEmpty(EmailTextBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông  tin!!!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (acc != null && acc.Password == PasswordBox.Password)
            {
                if (acc.Role == 1)
                {
                    HomePage homePage = new HomePage(EmailTextBox.Text , acc.Role);
                    homePage.Show();
                    this.Close();
                }
                else if (acc.Role == 2)
                {
                    HomePage homePage = new HomePage(EmailTextBox.Text , acc.Role);
                    homePage.Show();
                    this.Close();
                }
            }
           else
            {
                MessageBox.Show("Mật khẩu hoặc tài khoản không chính xác vui lòng thử lại!!!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // code-behind xu ly nut dang ki
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Close();
        }
        // xu li nut thoat
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

        private void ForgetPassButton_Click(object sender, RoutedEventArgs e)
        {
            var existingAccount = db.Accounts.FirstOrDefault(acc => acc.Email == EmailTextBox.Text);
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                MessageBox.Show("Bạn chưa nhập gmail của mình", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (existingAccount == null)
            {
                MessageBox.Show("Tài khoản không tồn tại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else {
                C_Send CS = new C_Send();
                var captchaCode = CS.GenerateCaptchaCode();
                CS.SendCaptchaEmail(EmailTextBox.Text, captchaCode);
                MessageBox.Show("Mật khẩu đã được gửi đến email", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                existingAccount.Password = captchaCode;
                db.SaveChanges();
            }
        }
    }
}

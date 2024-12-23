using Project_PRN212_FALL24.Models;
using Project_PRN212_FALL24.Validate;
using System.Windows;
using Project_PRN212_FALL24.HandleGmail;

namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        private string captchaCode;
        public Register()
        {
            InitializeComponent();
        }
        // code-behind xu ly nut dang ki
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var existingAccount = db.Accounts.FirstOrDefault(acc => acc.Email == EmailTextBox.Text);
            // Validate Start ----------------------------
            ValidateAll validate = new ValidateAll();
            if (existingAccount != null)
            {
                MessageBox.Show("Tài khoản đã tồn tại !!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (!validate.ValidateEmail(EmailTextBox.Text))
            {
                MessageBox.Show("Email không hợp lệ", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!validate.ValidatePassWord(PasswordBox.Password)) {
                MessageBox.Show("Mật khẩu phải lớn hơn 6 kí tự", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!PasswordBox.Password.Equals(ConfirmPasswordBox.Password)) {
                MessageBox.Show("Mật khẩu và Xác Nhận Mật Khẩu không giống nhau", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // Validate End ----------------------------
            else
            {
                C_Send CS = new C_Send();   
                captchaCode = CS.GenerateCaptchaCode();
                CS.SendCaptchaEmail(EmailTextBox.Text, captchaCode);
                this.Hide();

                //  Hiển thị cửa sổ nhập CAPTCHA
                CapchaWindow captchaWindow = new CapchaWindow(captchaCode, EmailTextBox.Text);
                if (captchaWindow.ShowDialog() == true) // Nếu người dùng nhập đúng mã
                {
                    var newAccount = new Account
                    {
                        Email = EmailTextBox.Text,
                        Password = PasswordBox.Password,
                        Role = 2
                    };

                    db.Accounts.Add(newAccount);
                    db.SaveChanges();

                    MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    HomePage homePage = new HomePage(EmailTextBox.Text , newAccount.Role);
                    homePage.Show();
                    this.Close();
                }
            }
        }
        // code-behind xu ly nut quay lai
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
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

    }
}

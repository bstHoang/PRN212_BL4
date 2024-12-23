
using System.Windows;
using Project_PRN212_FALL24.HandleGmail;
using Project_PRN212_FALL24.Models;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for CapchaWindow.xaml
    /// </summary>
    public partial class CapchaWindow : Window
    {
        private string ExpectecCaptcha;
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        private string userEmail;
        
        public CapchaWindow(string captchaCode, string email)
        {
            InitializeComponent();
            ExpectecCaptcha = captchaCode;
            userEmail = email;
            
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (CaptchaTextBox.Text == ExpectecCaptcha)
            {
                this.DialogResult = true; // Người dùng nhập đúng mã
                this.Close();
            }
            else
            {
                MessageBox.Show("Mã xác nhận không đúng. Vui lòng thử lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Resent_Click(object sender, RoutedEventArgs e)
        {
            C_Send CS = new C_Send();
            ExpectecCaptcha = CS.GenerateCaptchaCode();
            CS.SendCaptchaEmail(userEmail, ExpectecCaptcha);
            MessageBox.Show("Mã xác nhận mới đã được gửi qua email.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát không?",
                                              "Xác nhận thoát",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }
    }
}

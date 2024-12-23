using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_PRN212_FALL24.HandleGmail
{
    internal class C_Send
    {
        // Tạo mã CAPTCHA ngẫu nhiên
        public string GenerateCaptchaCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 6; i++) // Tạo mã dài 6 ký tự
            {
                builder.Append(chars[random.Next(chars.Length)]);
            }
            return builder.ToString();
        }

        // Gửi mã CAPTCHA qua email
        public void SendCaptchaEmail(string recipientEmail, string code)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("hoangbsthe186345@fpt.edu.vn");
                message.To.Add(recipientEmail);
                message.Subject = "Xác nhận tài khoản";
                message.Body = $"Mã xác thực của bạn là: {code}";
                message.IsBodyHtml = false;

                SmtpClient client = new SmtpClient("smtp.gmail.com"); // Thay bằng SMTP của email bạn
                client.Port = 587;
                client.Credentials = new NetworkCredential("hoangbsthe186345@fpt.edu.vn", "ttgp osbo bqyy zetm");// cam phai sua
                client.EnableSsl = true;
                client.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể gửi mã xác thực. Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}

using Microsoft.EntityFrameworkCore;
using Project_PRN212_FALL24.Models;
using System.Windows;
using System.Windows.Input;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for UserHistory.xaml
    /// </summary>
    public partial class UserHistory : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public UserHistory(string email)
        {
            InitializeComponent();
            LoadHistory();
            EmailTextBlock.Text = email;
            AdminAccountTextBlock.Visibility = Visibility.Collapsed;
        }

        private void LoadHistory() {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) { 
                var history = db.Histories.Include(x => x.IdbookNavigation).ToList();
                BookDataGrid.ItemsSource = history;
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ProjectPrn212Fall24Context())
            {
                var query = db.Histories
                    .Include(h => h.IdbookNavigation)
                    .AsQueryable();


                DateOnly? fromDate = DatePickerFrom.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(DatePickerFrom.SelectedDate.Value)
                    : null;

                DateOnly? toDate = DatePickerTo.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(DatePickerTo.SelectedDate.Value)
                    : null;

                if (fromDate.HasValue && toDate.HasValue)
                {
                    query = query.Where(h => h.BookLoanDate >= fromDate.Value &&
                                             h.BookLoanDate <= toDate.Value);
                }
                else if (fromDate.HasValue)
                {
                    query = query.Where(h => h.BookLoanDate >= fromDate.Value);
                }
                else if (toDate.HasValue)
                {
                    query = query.Where(h => h.BookLoanDate <= toDate.Value);
                }

                var result = query.ToList();
                BookDataGrid.ItemsSource = result;
            }
        }
    }
}

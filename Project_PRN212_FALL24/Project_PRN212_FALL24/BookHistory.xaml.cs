using Project_PRN212_FALL24.Models;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for BookHistory.xaml
    /// </summary>
    public partial class BookHistory : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public BookHistory(string email)
        {
            InitializeComponent();
            LoadBookHistory();
            EmailTextBlock.Text = email;
            AdminAccountTextBlock.Visibility = Visibility.Collapsed;
        }
        private void LoadBookHistory() {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) { 
                var bookHistory = db.Histories.Include(x => x.IdaccountNavigation)
                    .Include(x => x.IdbookNavigation).ToList();
                BookHistoryDataGrid.ItemsSource = bookHistory;
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

        private void ManageNookLoan_Click(object sender, RoutedEventArgs e)
        {
            BookHistory bookHistory = new BookHistory(EmailTextBlock.Text);
            bookHistory.Show();
            this.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dpkBRD.SelectedDate == null)
            {
                MessageBox.Show("Ngày chưa được chọn.");
                return; 
            }

            if (string.IsNullOrWhiteSpace(TextBoxID.Text))
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để cập nhật.");
                return;
            }

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                if (int.TryParse(TextBoxID.Text, out int historyId))
                {
                    var existHistory = db.Histories.FirstOrDefault(x => x.Id == historyId);

                    if (existHistory != null) {

                        if (dpkBRD.SelectedDate.Value < existHistory.BookLoanDate.ToDateTime(TimeOnly.MinValue))
                        {
                            MessageBox.Show("Ngày trả phải sau ngày thuê.");
                            return;
                        }

                        existHistory.BookReturnDate = DateOnly.FromDateTime(dpkBRD.SelectedDate.Value);

                        db.SaveChanges();
                        MessageBox.Show("Cập nhật lịch sử giao dịch thành công!");
                        LoadBookHistory();
                        ClearInput();
                    }
                }
                
            }
        }


        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxID.Text))
            {
                MessageBox.Show("Vui lòng chọn một sách để xóa.");
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?",
                                        "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
                {
                    var bookHistoryId = int.Parse(TextBoxID.Text);
                    var bookHistoryToDelete = db.Histories.FirstOrDefault(b => b.Id == bookHistoryId);

                    if (bookHistoryToDelete != null)
                    {
                        db.Histories.Remove(bookHistoryToDelete);
                        db.SaveChanges();
                        MessageBox.Show("Xóa sách thành công!");
                        LoadBookHistory();
                        ClearInput();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sách để xóa.");
                    }
                }
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInput();
        }
        private void ClearInput(){
            TextBoxID.Clear();
            TextBoxEmail.Clear();
            TextBoxBookId.Clear();
            TextBoxBookName.Clear();
            dpkBRD.SelectedDate = null;
            dpkBLD.SelectedDate = null;
        }

        private void BookHistoryDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BookHistoryDataGrid.SelectedValue is History selectedHistory)
            {
                TextBoxID.Text = selectedHistory.Id.ToString();
                TextBoxEmail.Text = selectedHistory.IdaccountNavigation.Email;
                TextBoxBookId.Text = selectedHistory.IdbookNavigation.Id.ToString();
                TextBoxBookName.Text = selectedHistory.IdbookNavigation.Name;
                dpkBRD.SelectedDate = selectedHistory.BookReturnDate?.ToDateTime(TimeOnly.MinValue);
                dpkBLD.SelectedDate = selectedHistory.BookLoanDate.ToDateTime(TimeOnly.MinValue);
            }
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
                BookHistoryDataGrid.ItemsSource = result;
            }
        }

    }
}
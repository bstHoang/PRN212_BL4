
using Microsoft.EntityFrameworkCore;
using Project_PRN212_FALL24.Models;
using System.Windows;
using System.Windows.Input;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage(string email, int accRole)
        {
            InitializeComponent();
            LoadComboboxFilterAuthor();
            LoadComboboxFilterType();
            LoadBook();
            EmailTextBlock.Text = email;

            if (accRole == 1)
            {
                AdminAccountTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                AdminAccountTextBlock.Visibility = Visibility.Collapsed;
            }
        }
        private void LoadComboboxFilterType()
        {

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var types = db.Settings.Where(x => x.Type == 2).ToList();

                var allType = new Setting { Id = 0, Name = "All" };

                types.Insert(0, allType);

                ComboBoxTypeFilter.ItemsSource = types;

                ComboBoxTypeFilter.SelectedIndex = 0;

            }
        }
        private void LoadComboboxFilterAuthor()
        {
            using (var db = new ProjectPrn212Fall24Context())
            {
                var authors = db.Books
                                .Where(b => !string.IsNullOrEmpty(b.Author))
                                .Select(b => b.Author)
                                .Distinct()
                                .ToList();
                authors.Insert(0, "All");

                ComboBoxAuthorFilter.ItemsSource = authors;

                ComboBoxAuthorFilter.SelectedIndex = 0;
            }
        }
        private void LoadBook()
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var bookList = db.Books
                    .Include(x => x.TypeNavigation)
                    .ToList();

                BookDataGrid.ItemsSource = bookList;
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
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var query = db.Books
                    .Include(x => x.TypeNavigation)
                    .Include(x => x.CreateByNavigation)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(TextBoxSearch.Text))
                {
                    query = query.Where(b => b.Name.Contains(TextBoxSearch.Text));
                }

                if (ComboBoxTypeFilter.SelectedIndex > 0)
                {
                    int selectedType = (int)ComboBoxTypeFilter.SelectedValue;
                    query = query.Where(b => b.Type == selectedType);
                }

                if (ComboBoxAuthorFilter.SelectedIndex > 0)
                {
                    string selectedAuthor = ComboBoxAuthorFilter.SelectedItem.ToString();
                    if (selectedAuthor != "All") // Kiểm tra nếu không phải là "All"
                    {
                        query = query.Where(b => b.Author == selectedAuthor);
                    }
                }

                var result = query.ToList();

                BookDataGrid.ItemsSource = result;
            }
        }

        private void BookDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BookDataGrid.SelectedItem is Book SeletedBook)
            {
                TextBoxId.Text = SeletedBook.Id.ToString();
                TextBoxName.Text = SeletedBook.Name;
                TextType.Text = SeletedBook.TypeNavigation.Name;
                TextBoxAuthor.Text = SeletedBook.Author;
                TxtQuantity.Text = SeletedBook.Quantity.ToString();
                TextBoxDescription.Text = SeletedBook.Description;
            }
        }

        private void btnBrow_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var existAcc = db.Accounts.FirstOrDefault(x => x.Email == EmailTextBlock.Text);
                var existBook = db.Histories.Where(x => x.Idaccount == existAcc.Id)
                    .FirstOrDefault(x => x.Idbook == int.Parse(TextBoxId.Text));
                var bookHasLoan = db.Books.FirstOrDefault(x => x.Id == int.Parse(TextBoxId.Text));

                if (BookDataGrid.SelectedValue is Book seletedBook) {
                    if (seletedBook.Quantity <= 0) {
                        MessageBox.Show("Sách đã được mượn hết");
                    }
                    else if (existBook != null && existBook.BookReturnDate == null)
                    {
                        MessageBox.Show("Bạn đã mượn sách này rồi nhưng chưa trả lại");
                    }
                    else
                    {
                        var history = new History {
                            Idaccount = existAcc.Id,
                            Idbook = int.Parse(TextBoxId.Text),
                            BookLoanDate = DateOnly.FromDateTime(DateTime.Now),
                            BookReturnDate = null,
                            Deadline = DateOnly.FromDateTime(DateTime.Now)
                        };
                        bookHasLoan.Quantity -= 1;

                        db.Histories.Add(history);
                        db.SaveChanges();
                        MessageBox.Show("Muợn thành công");
                        ClearInputs();
                        LoadBook();
                    }
                }
            }
        }

        private void ClearInputs(){
            TextBoxId.Clear();
            TextBoxName.Clear();
            TextType.Clear();
            TextBoxAuthor.Clear();
            TxtQuantity.Clear();
            TextBoxDescription.Clear();
        }
    }
}

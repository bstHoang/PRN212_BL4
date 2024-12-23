using Microsoft.EntityFrameworkCore;
using Project_PRN212_FALL24.Models;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;


namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for ManageBook.xaml
    /// </summary>
    public partial class ManageBook : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public ManageBook(string email)
        {
            InitializeComponent();
            LoadBook();
            LoadComboboxAcc();
            LoadComboboxTypeBook();
            LoadComboboxFilterAuthor();
            LoadComboboxFilterType();
            EmailTextBlock.Text = email;
            AdminAccountTextBlock.Visibility = Visibility.Collapsed;
        }

        private void LoadComboboxFilterType() {
            
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

        private void LoadBook()
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var bookList = db.Books
                    .Include(x => x.TypeNavigation)
                    .Include(x => x.CreateByNavigation)
                    .ToList();

                BookDataGrid.ItemsSource = bookList;
            }
        }

        private void LoadComboboxAcc()
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var acc = db.Accounts.Where(x => x.Role == 1).ToList();
                ComboBoxCreateBy.ItemsSource = acc;
            }
        }
        private void LoadComboboxTypeBook()
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var type = db.Settings.Where(x => x.Type == 2).ToList();
                ComboBoxType.ItemsSource = type;
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TextBoxName.Text) ||
                ComboBoxType.SelectedValue == null ||
                string.IsNullOrEmpty(TextBoxAuthor.Text) ||
                string.IsNullOrEmpty(TextBoxDescription.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (!int.TryParse(TxtQuantity.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên lớn hơn 0.");
                return;
            }

            var newBook = new Book
            {
                Name = TextBoxName.Text,
                CreateBy = Convert.ToInt32(ComboBoxCreateBy.SelectedValue),
                Type = Convert.ToInt32(ComboBoxType.SelectedValue),
                Image = "aaa",
                Author = TextBoxAuthor.Text,
                Description = TextBoxDescription.Text,
                DateCreate = DateOnly.FromDateTime(DateTime.Now),
                DateModify = DateOnly.FromDateTime(DateTime.Now),
                Quantity = Convert.ToInt32(TxtQuantity.Text)
            };

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                db.Books.Add(newBook);
                db.SaveChanges();
                MessageBox.Show("Thêm sách thành công!");
                LoadBook();
                ClearInputs();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            // Kiểm tra ID có trống không
            if (string.IsNullOrWhiteSpace(TextBoxID.Text))
            {
                MessageBox.Show("Vui lòng chọn một sách để cập nhật.");
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtQuantity.Text))
            {
                MessageBox.Show("Vui nhập số lượng");
                return;
            }

            // Validate các trường không được để trống
            if (string.IsNullOrWhiteSpace(TextBoxName.Text))
            {
                MessageBox.Show("Tên sách không được để trống.");
                return;
            }

            if (ComboBoxType.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại sách.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBoxAuthor.Text))
            {
                MessageBox.Show("Tên tác giả không được để trống.");
                return;
            }

            if (string.IsNullOrWhiteSpace(TextBoxDescription.Text))
            {
                MessageBox.Show("Mô tả không được để trống.");
                return;
            }
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var bookId = int.Parse(TextBoxID.Text);
                var existingBook = db.Books.FirstOrDefault(b => b.Id == bookId);

                if (existingBook != null)
                {
                    existingBook.Name = TextBoxName.Text;
                    existingBook.Type = Convert.ToInt32(ComboBoxType.SelectedValue);
                    existingBook.DateModify = DateOnly.FromDateTime(DateTime.Now);
                    existingBook.Quantity = Convert.ToInt32(TxtQuantity.Text);
                    existingBook.Author = TextBoxAuthor.Text;
                    existingBook.Description = TextBoxDescription.Text;

                    db.SaveChanges();
                    MessageBox.Show("Cập nhật sách thành công!");
                    LoadBook();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sách cần cập nhật.");
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
                    var bookId = int.Parse(TextBoxID.Text);
                    var bookToDelete = db.Books.FirstOrDefault(b => b.Id == bookId);

                    if (bookToDelete != null)
                    {
                        db.Books.Remove(bookToDelete);
                        db.SaveChanges();
                        MessageBox.Show("Xóa sách thành công!");
                        LoadBook();
                        ClearInputs();
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
            ClearInputs();
        }

        private void BookDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BookDataGrid.SelectedItem is Book SeletedBook) {
                TextBoxID.Text = SeletedBook.Id.ToString();
                ComboBoxCreateBy.SelectedValue = SeletedBook.CreateBy.ToString();
                TextBoxName.Text = SeletedBook.Name;
                ComboBoxType.SelectedValue = SeletedBook.Type.ToString();
                TextBoxAuthor.Text = SeletedBook.Author;
                TxtQuantity.Text = SeletedBook.Quantity.ToString();
                TextBoxDescription.Text = SeletedBook.Description;
                dpkDCreate.SelectedDate = SeletedBook.DateCreate.ToDateTime(TimeOnly.MinValue);
                dpkDModify.SelectedDate = SeletedBook.DateModify?.ToDateTime(TimeOnly.MinValue);
            }
        }

        private void ClearInputs()
        {
            TextBoxID.Clear();
            TextBoxName.Clear(); 
            ComboBoxCreateBy.SelectedIndex = -1; 
            ComboBoxType.SelectedIndex = -1;
            dpkDCreate.SelectedDate = null;
            dpkDModify.SelectedDate = null; 
            TextBoxAuthor.Clear();
            TextBoxDescription.Clear(); 
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new ProjectPrn212Fall24Context())
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

using Project_PRN212_FALL24.Models;
using System.Windows;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Project_PRN212_FALL24
{
    /// <summary>
    /// Interaction logic for ManageTypeBook.xaml
    /// </summary>
    public partial class ManageTypeBook : Window
    {
        ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context();
        public ManageTypeBook(string email)
        {
            InitializeComponent();
            LoadTypeBook();
            EmailTextBlock.Text = email;
            AdminAccountTextBlock.Visibility = Visibility.Collapsed;
        }
        private void LoadTypeBook()
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var typeBook = db.Settings.Where(x => x.Type == 2).ToList();
                BookTypeDataGrid.ItemsSource = typeBook;
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

        private void BookTypeDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (BookTypeDataGrid.SelectedItem is Setting SelectedSetting) {
                txtID.Text = SelectedSetting.Id.ToString();
                txtName.Text = SelectedSetting.Name;
            }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if ( string.IsNullOrEmpty(txtName.Text)) {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }

            var bookType = new Setting
            {
                Type = 2,
                Name = txtName.Text
            };

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                var existBookType = db.Settings.Where(x => x.Name.Equals(txtName.Text));

                if (existBookType != null)
                {
                    MessageBox.Show("Loại sách này đã tồn tại rồi");
                }
                else
                {
                    db.Add(bookType);
                    db.SaveChanges();
                    MessageBox.Show("Thêm loại sách mới thành công");
                    LoadTypeBook();
                    ClearInputs();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một loại sách để cập nhật.");
                return;
            }

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }

            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context()) {
                var bookTypeId = int.Parse(txtID.Text);
                var bookType = db.Settings.FirstOrDefault(x => x.Id == bookTypeId);

                var existBookType = db.Settings.Where(x => x.Name.Equals(txtName.Text));
                if (existBookType != null)
                {
                    MessageBox.Show("Loại sách này đã tồn tại rồi");
                }

                else if (bookType != null) {
                    bookType.Name = txtName.Text;
                    db.SaveChanges();
                    MessageBox.Show("Chỉnh sủa danh sách thành công");
                    LoadTypeBook();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại sách cần cập nhật.");
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn một loại sách để xoá.");
                return;
            }
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?",
                                        "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
                {
                    var bookTypeID = int.Parse(txtID.Text);
                    var bookTypetoDelete = db.Settings.FirstOrDefault(x => x.Id == bookTypeID);

                    if (bookTypetoDelete != null)
                    {
                        db.Settings.Remove(bookTypetoDelete);
                        db.SaveChanges();
                        MessageBox.Show("Xóa loại sách thành công!");
                        LoadTypeBook();
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy loại sách để xóa.");
                    }
                }
    }
}
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs() {
            txtID.Clear();
            txtName.Clear();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            using (ProjectPrn212Fall24Context db = new ProjectPrn212Fall24Context())
            {
                var query = db.Settings.Where(x => x.Type == 2).AsQueryable();

                if (!string.IsNullOrEmpty(TextBoxSearch.Text))
                {
                    query = query.Where(b => b.Name.Contains(TextBoxSearch.Text));
                }
                var result = query.ToList();
                BookTypeDataGrid.ItemsSource = result;
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
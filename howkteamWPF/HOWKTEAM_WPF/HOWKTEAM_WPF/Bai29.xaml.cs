using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HOWKTEAM_WPF
{
    /// <summary>
    /// Interaction logic for Bai29.xaml
    /// </summary>
    public partial class Bai29 : Window
    {
        public Bai29()
        {
            InitializeComponent();
            List<User2> items = new List<User2>();
            items.Add(new User2() { Name = "HowKteam", Age = 42 });
            items.Add(new User2() { Name = "Kteam", Age = 39 });
            items.Add(new User2() { Name = "Free Education", Age = 13 });
            items.Add(new User2() { Name = "Share to be better", Age = 13 });
            lvUsers.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            view.Filter = UserFilter;
        }

        private bool UserFilter(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as User2).Name.IndexOf(txtFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void txtFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lvUsers.ItemsSource).Refresh();
        }
    }

    public enum SexType2 { Male, Female };

    public class User2
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Mail { get; set; }

        public SexType2 Sex { get; set; }
    }
}
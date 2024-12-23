using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Bai28.xaml
    /// </summary>
    public partial class Bai28 : Window
    {
        public bool IsSort;
        public Bai28()
        {
            InitializeComponent();
            IsSort = false;
            List<User1> items = new List<User1>();
            items.Add(new User1() { Name = "HowKteam.com", Age = 42 });
            items.Add(new User1() { Name = "Kteam", Age = 13 });
            items.Add(new User1() { Name = "Free Education", Age = 39 });
            items.Add(new User1() { Name = "Share to be better", Age = 13 });
            lvUsers.ItemsSource = items;

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUser1s.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUser1s.ItemsSource);
            //view.SortDescriptions.Add(new SortDescription("Age", ListSortDirection.Ascending));
            //view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader header = sender as GridViewColumnHeader;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            if (IsSort)
            {
                //view.SortDescriptions.Remove(new SortDescription(header.Content.ToString(), ListSortDirection.Descending));
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(header.Content.ToString(), ListSortDirection.Ascending));
            }
            else
            {
                //view.SortDescriptions.Remove(new SortDescription(header.Content.ToString(), ListSortDirection.Ascending));
                view.SortDescriptions.Clear();
                view.SortDescriptions.Add(new SortDescription(header.Content.ToString(), ListSortDirection.Descending));
            }

            IsSort = !IsSort;
        }
    }

    public class User1
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
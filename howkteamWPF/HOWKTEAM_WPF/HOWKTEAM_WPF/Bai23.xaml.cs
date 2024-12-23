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
    /// Interaction logic for Bai23.xaml
    /// </summary>
    public partial class Bai23 : Window
    {
        List<Food> listName;
        public Bai23()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listName = new List<Food>() {
            new Food(){Name = "Mực một nắng", Price = "200.000"},
            new Food(){Name = "Ếch núp lùm", Price = "50.000"},
            new Food(){Name = "Dú dê nướng sữa", Price = "75.000"}
            };
            cbItemSource.ItemsSource = listName;
            //cbItemSource.DisplayMemberPath = "Name";
            //cbItemSource.SelectedValuePath = "Price";

            cbColor.ItemsSource = typeof(Brushes).GetProperties();

            cbItemSource.SelectionChanged += cbItemSource_SelectionChanged;
        }

        void cbItemSource_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(cbItemSource.SelectedValue.ToString());
        }
    }
    public class Food
    {
        public string Name { get; set; }
        public string Price { get; set; }
    }
}
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
    /// Interaction logic for Bai20.xaml
    /// </summary>
    public partial class Bai20 : Window
    {
        public Bai20()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            img.Source = new BitmapImage(new Uri(txbSource.Text));
        }
    }
}
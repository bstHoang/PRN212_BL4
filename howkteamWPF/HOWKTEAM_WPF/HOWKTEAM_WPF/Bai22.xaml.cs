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
    /// Interaction logic for Bai22.xaml
    /// </summary>
    public partial class Bai22 : Window
    {
        public Bai22()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str1 = "www.howkteam.com";
            string str2 = "Free Education";

            btn1.DataContext = str1;
            btn2.DataContext = str2;
        }
    }
}
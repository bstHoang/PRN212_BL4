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
    /// Interaction logic for Bai15.xaml
    /// </summary>
    public partial class Bai15 : Window
    {
        public Bai15()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // get scroll bar offset value
            //MessageBox.Show(scvMain.VerticalOffset.ToString());

            // get viewport height
            //MessageBox.Show(scvMain.ViewportHeight.ToString());

            // scroll to end
            //scvMain.ScrollToEnd();

            // maximum scroll offset
            //MessageBox.Show(scvMain.ScrollableHeight.ToString());           
        }
    }
}
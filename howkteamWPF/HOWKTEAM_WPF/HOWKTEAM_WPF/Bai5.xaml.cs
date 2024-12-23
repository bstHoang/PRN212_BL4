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
    /// Interaction logic for Bai5.xaml
    /// </summary>
    public partial class Bai5 : Window
    {
        public Bai5()
        {
            InitializeComponent();
        }

        private void btnValue_Click(object sender, RoutedEventArgs e)
        {
            txblValue.Text = txbValue.Text;
        }
        int DoubleValue(int value)
        {
            return value * 2;
        }
        private void txbValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value = 0;
            if (int.TryParse(txbValue.Text, out value))
            {
                txblValue.Text = DoubleValue(value).ToString();
            }
            else
            {
                txblValue.Text = "Vui lòng nhập giá trị chính xác!";
            }
        }
    }
}
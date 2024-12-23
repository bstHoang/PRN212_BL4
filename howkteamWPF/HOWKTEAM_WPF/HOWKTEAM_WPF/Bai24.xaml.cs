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
    /// Interaction logic for Bai24.xaml
    /// </summary>
    public partial class Bai24 : Window
    {
        List<string> ListData;
        public Bai24()
        {
            InitializeComponent();
            ListData = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                ListData.Add(i.ToString());
            }
            cbCombo.ItemsSource = ListData;
            lsbList.ItemsSource = ListData;
        }
    }
}
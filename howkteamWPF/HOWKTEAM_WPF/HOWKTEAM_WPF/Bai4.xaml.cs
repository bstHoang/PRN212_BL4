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
    /// Interaction logic for Bai4.xaml
    /// </summary>
    public partial class Bai4 : Window
    {
        public Bai4()
        {
            InitializeComponent();
            Button btn = new Button();
            btn.Click += btn_Click;
            //btn.Content = "Free Education";
            grdButton.Children.Add(btn);

            //TextBlock txbl = new TextBlock();
            //txbl.Text = "Share to be better";
            //btn.Content = txbl;

            //TextBox txb = new TextBox();
            //txb.Width = 100;
            //txb.Height = 50;
            //btn.Content = txb;
        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("CLick rồi nè!");
        }
    }
}
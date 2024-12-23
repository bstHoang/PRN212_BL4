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
    /// Interaction logic for Bai37.xaml
    /// </summary>
    public partial class Bai37 : Window
    {
        private string dataValue;
        public string DataValue
        {
            get { return dataValue; }
            set { dataValue = value; OnPropertyChanged("DataValue"); }
        }
        public Bai37()
        {
            InitializeComponent();
            DataValue = "www.howkteam.com";
            this.DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace ProjektWPF
{
    /// <summary>
    /// Logika interakcji dla klasy AlertCreator.xaml
    /// </summary>
    public partial class AlertCreator : Window
    {
        public Models.Alert alert;
        public AlertCreator()
        {
            InitializeComponent();
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            if(Alert_date.Text != "")
            {
                alert = new Models.Alert(Task_name.Text, DateTime.Parse(Alert_date.Text));
                DialogResult = true;
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

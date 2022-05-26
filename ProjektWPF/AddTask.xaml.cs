using ProjektWPF.Models;
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

namespace ProjektWPF
{
    /// <summary>
    /// Interaction logic for AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        List<Category> categories = new List<Category>();
        public AddTask(List<Category> categories)
        {
            this.categories = categories;
            InitializeComponent();
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void check_Checked(object sender, RoutedEventArgs e)
        {
            if(check.IsChecked==true)
            {
                sdate.IsEnabled = true;
                edate.IsEnabled = true;
            } else
            {
                sdate.IsEnabled = false;
                edate.IsEnabled = false;
            }
        }

        private void importance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (importance.Value == 1)
            {

            } else if (importance.Value == 2)
            {

            }
            else if (importance.Value == 3)
            {

            }
            else if (importance.Value == 4)
            {

            }
            else if (importance.Value == 5)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (name != null)
            {
                if (sdate != null && edate== null)
                {
                    Models.Task task = new Models.Task(name.Text, (int)importance.Value, sdate.SelectedDate.Value);
                    this.Close();
                }
                else if (sdate != null && edate != null)
                {
                    Models.Task task = new Models.Task(name.Text, (int)importance.Value, sdate.SelectedDate.Value, edate.SelectedDate.Value);
                    this.Close();
                }
                else
                {
                    Models.Task task = new Models.Task(name.Text, (int)importance.Value);
                    this.Close();
                }
            } else
            {
                MessageBox.Show("Incorrectly completed form.");
            }
        }
    }
}

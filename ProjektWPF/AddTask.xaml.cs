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
        public Models.Task task;

        public AddTask(List<Category> categories)
        {
            InitializeComponent();
            category.DataContext = categories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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
                sdate.SelectedDate = null;
                edate.IsEnabled = false;
                edate.SelectedDate = null;
            }
        }

        private void importance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            if (importance.Value == 0)
            {

            } else if (importance.Value == 1)
            {

            }
            else if (importance.Value == 2)
            {

            }
            else if (importance.Value == 3)
            {

            }
            else if (importance.Value == 4)
            {

            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (name.Text != "" && category.SelectedItem != null)
            {
                if (sdate.SelectedDate != null && edate.SelectedDate == null)
                {
                    task = new Models.Task(name.Text, (int)importance.Value, sdate.SelectedDate.Value, category.SelectedItem as Category);
                    DialogResult = true;
                }
                else if (sdate.SelectedDate != null && edate.SelectedDate != null)
                {
                    task = new Models.Task(name.Text, (int)importance.Value, sdate.SelectedDate.Value, edate.SelectedDate.Value, category.SelectedItem as Category);
                    DialogResult = true;
                }
                else
                {
                    task = new Models.Task(name.Text, (int)importance.Value, category.SelectedItem as Category);
                    DialogResult = true;
                }
            } else
            {
                MessageBox.Show("Incorrectly completed form.");
            }
        }
    }
}

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
    /// Interaction logic for AddSubtask.xaml
    /// </summary>
    public partial class AddSubtask : Window
    {
        public SubTask subtask;
        private static DateTime startDate = new DateTime(default);
        private static DateTime endDate = DateTime.MaxValue;

        public static DateTime StartDate
        {
            get
            {
                return startDate;
            }
        }

        public static DateTime EndDate
        {
            get
            {
                return endDate;
            }
        }

        public AddSubtask()
        {
            InitializeComponent();
        }
        public AddSubtask(DateTime sdate)
        {
            startDate = sdate;
            InitializeComponent();

        }
        public AddSubtask(DateTime sdate, DateTime edate)
        {
            startDate = sdate;
            endDate = edate;
            InitializeComponent();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void check_Checked(object sender, RoutedEventArgs e)
        {
            if (check.IsChecked == true)
            {
                sdate.IsEnabled = true;
                edate.IsEnabled = true;
            }
            else
            {
                sdate.IsEnabled = false;
                sdate.SelectedDate = null;
                edate.IsEnabled = false;
                edate.SelectedDate = null;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            if (name.Text != "")
            {
                if (sdate.SelectedDate != null && edate.SelectedDate == null)
                {
                    subtask = new SubTask(name.Text, sdate.SelectedDate.Value);
                    DialogResult = true;
                }
                else if (sdate.SelectedDate != null && edate.SelectedDate != null)
                {
                    subtask = new SubTask(name.Text, sdate.SelectedDate.Value, edate.SelectedDate.Value);
                    DialogResult = true;
                }
                else
                {
                    subtask = new SubTask(name.Text);
                    DialogResult = true;
                }
            }
            else
            {
                MessageBox.Show("Incorrectly completed form.");
            }
        }
    }
}

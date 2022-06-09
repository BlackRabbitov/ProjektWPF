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
        public AddSubtask()
        {
            InitializeComponent();
        }
        public AddSubtask(DateTime sdate)
        {
            InitializeComponent();
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, sdate.AddDays(-1));
            this.sdate.BlackoutDates.Add(cdr);
            this.edate.BlackoutDates.Add(cdr);

        }
        public AddSubtask(DateTime sdate, DateTime edate)
        {
            InitializeComponent();
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, sdate.AddDays(-1));
            CalendarDateRange cdr2 = new CalendarDateRange(edate.AddDays(1), DateTime.MaxValue);
            this.sdate.BlackoutDates.Add(cdr);
            this.edate.BlackoutDates.Add(cdr);
            this.sdate.BlackoutDates.Add(cdr2);
            this.edate.BlackoutDates.Add(cdr2);

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

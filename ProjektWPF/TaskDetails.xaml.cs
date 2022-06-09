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
    /// Logika interakcji dla klasy TaskDetails.xaml
    /// </summary>
    public partial class TaskDetails : Window
    {
        private Models.Task SourceTask { get; set; }
        public TaskDetails()
        {
            InitializeComponent();
        }

        private void close_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void edit_subtask_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void remove_subtask_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_subtask_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void add_alarm_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void edit_alarm_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void remove_alarm_button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void SetSourceTask(Models.Task task)
        {
            SourceTask = task;
            name.DataContext = SourceTask;
            importance.DataContext = SourceTask;
            category.DataContext = SourceTask;
            if(SourceTask.StartDate == null)
            {
                start_date.Content = "NaN";
            } else
            {
                start_date.DataContext = SourceTask;
            }
            if (SourceTask.EndDate == null)
            {
                end_date.Content = "NaN";
            } else
            {
                end_date.DataContext = SourceTask;
            }
            subtasks_listbox.DataContext = SourceTask;
            alarms_listbox.DataContext = SourceTask;
        }
    }
}

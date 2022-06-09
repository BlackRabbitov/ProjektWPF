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
            this.DialogResult = true;
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
            AlertCreator alertCreator = new AlertCreator();
            alertCreator.Task_name.Text = SourceTask.Name;

            if (alertCreator.ShowDialog() == true)
            {
                if(SourceTask.Alerts == null)
                {
                    SourceTask.Alerts = new List<Models.Alert>();
                }

                SourceTask.Alerts.Add(alertCreator.alert);

                alarms_listbox.Items.Refresh();
            }
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
            subtasks_listbox.ItemsSource = SourceTask.SubTasks;
            alarms_listbox.ItemsSource = SourceTask.Alerts;
            if (SourceTask.StartDate == null)
            {
                start_date.Text = "NaN";
            } else
            {
                start_date.DataContext = SourceTask;
            }
            if (SourceTask.EndDate == null)
            {
                end_date.Text = "NaN";
            } else
            {
                end_date.DataContext = SourceTask;
            }
        }

        private void edit_task_button_Click(object sender, RoutedEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            AddTask taskWindow = new AddTask(window.categories);

            taskWindow.name.Text = SourceTask.Name;
            taskWindow.importance.Value = SourceTask.Importance;
            if (SourceTask.StartDate != new DateTime())
            {
                taskWindow.check.IsChecked = true;
                taskWindow.sdate.SelectedDate = SourceTask.StartDate;
                taskWindow.edate.SelectedDate = SourceTask.EndDate;
            }
            else
            {
                taskWindow.check.IsChecked = false;
            }

            taskWindow.category.SelectedItem = SourceTask.Category;
            taskWindow.add.Content = "Modify";

            if (taskWindow.ShowDialog() == true)
            {
                SourceTask.Name = taskWindow.name.Text;
                SourceTask.Importance = (int)taskWindow.importance.Value;
                if (taskWindow.check.IsChecked != false)
                {
                    SourceTask.StartDate = taskWindow.sdate.SelectedDate.Value;
                    SourceTask.EndDate = taskWindow.edate.SelectedDate.Value;
                }
                SourceTask.Category = taskWindow.category.SelectedItem as Models.Category;
                window.Tasks_ListBox.Items.Refresh();

                name.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                importance.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                category.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                start_date.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                end_date.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                subtasks_listbox.GetBindingExpression(ListBox.ItemsSourceProperty).UpdateTarget();
            }
            
        }
    }
}

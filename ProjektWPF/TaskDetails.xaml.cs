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
            SubTask subtaskSelected = subtasks_listbox.SelectedItem as SubTask;

            if (subtaskSelected != null)
            {
                AddSubtask addSub = new AddSubtask();
                addSub.name.Text = subtaskSelected.Name;
                addSub.sdate.SelectedDate = subtaskSelected.StartDate;
                addSub.edate.SelectedDate = subtaskSelected.EndDate;

                if (addSub.ShowDialog() == true)
                {
                    subtaskSelected.Name = addSub.name.Text;
                    subtaskSelected.StartDate = addSub.sdate.SelectedDate.Value;
                    subtaskSelected.EndDate = addSub.edate.SelectedDate.Value;
                    subtasks_listbox.Items.Refresh();
                }
            }
        }

        private void remove_subtask_button_Click(object sender, RoutedEventArgs e)
        {
            SubTask subtaskSelected = subtasks_listbox.SelectedItem as SubTask;
            if (subtasks_listbox != null)
            {
                SourceTask.SubTasks.Remove(SourceTask.SubTasks.Find(x => x.Name == subtaskSelected.Name));
                subtasks_listbox.Items.Refresh();
            }

        }

        private void add_subtask_button_Click(object sender, RoutedEventArgs e)
        {
            if (SourceTask.StartDate != null && SourceTask.EndDate != null)
            {
                AddSubtask addSubtask = new AddSubtask(SourceTask.StartDate, SourceTask.EndDate);
                if (addSubtask.ShowDialog() == true)
                {
                    SourceTask.SubTasks.Add(addSubtask.subtask);
                    subtasks_listbox.Items.Refresh();
                }
            }
            else if (SourceTask.StartDate != null && SourceTask.EndDate == default(DateTime))
            {
                AddSubtask addSubtask = new AddSubtask(SourceTask.StartDate);
                if (addSubtask.ShowDialog() == true)
                {
                    SourceTask.SubTasks.Add(addSubtask.subtask);
                    subtasks_listbox.Items.Refresh();
                }
            }
            else
            {
                AddSubtask addSubtask = new AddSubtask();
                if (addSubtask.ShowDialog() == true)
                {
                    SourceTask.SubTasks.Add(addSubtask.subtask);
                    subtasks_listbox.Items.Refresh();
                }
            }
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

                alarms_listbox.ItemsSource = SourceTask.Alerts;
                alarms_listbox.Items.Refresh();
            }
        }

        private void edit_alarm_button_Click(object sender, RoutedEventArgs e)
        {
            Models.Alert selectedAlert = alarms_listbox.SelectedItem as Models.Alert;

            if (selectedAlert != null)
            {
                AlertCreator alertEditor = new AlertCreator();
                alertEditor.Task_name.Text = selectedAlert.Name;
                alertEditor.Alert_date.Text = selectedAlert.DateTime.ToString();

                if (alertEditor.ShowDialog() == true)
                {
                    selectedAlert.Name = alertEditor.Task_name.Text;
                    selectedAlert.DateTime = DateTime.Parse(alertEditor.Alert_date.Text);

                    alarms_listbox.ItemsSource = SourceTask.Alerts;
                    alarms_listbox.Items.Refresh();
                }
            }
        }

        private void remove_alarm_button_Click(object sender, RoutedEventArgs e)
        {
            Models.Alert alertSelected = alarms_listbox.SelectedItem as Models.Alert;
            if (alertSelected != null)
            {
                SourceTask.Alerts.Remove(SourceTask.Alerts.Find(x => x.Name == alertSelected.Name));
                alarms_listbox.Items.Refresh();
            }
        }

        public void SetSourceTask(Models.Task task)
        {
            SourceTask = task;
            name.DataContext = SourceTask;
            importance.DataContext = SourceTask;
            category.DataContext = SourceTask.Category;
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
            taskWindow.subtasks = SourceTask.SubTasks;
            taskWindow.subtasks_list.ItemsSource = taskWindow.subtasks;
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
                SourceTask.SubTasks = taskWindow.subtasks;
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

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
        public List<SubTask> subtasks = new List<SubTask>();

        public AddTask(List<Category> categories)
        {
            InitializeComponent();
            category.DataContext = categories;
            subtasks_list.DataContext = subtasks;
            imp_text.Text = "not important";
            BrushConverter bc = new BrushConverter();
            imp_text.Foreground = (Brush)bc.ConvertFrom("#C3F4CA");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
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
                imp_text.Text ="not important";
                BrushConverter bc = new BrushConverter();
                imp_text.Foreground = (Brush)bc.ConvertFrom("#C3F4CA");

            } else if (importance.Value == 1)
            {
                imp_text.Text = "a little important";
                BrushConverter bc = new BrushConverter();
                imp_text.Foreground = (Brush)bc.ConvertFrom("#F5F08E");
            }
            else if (importance.Value == 2)
            {
                imp_text.Text = "important";
                BrushConverter bc = new BrushConverter();
                imp_text.Foreground = (Brush)bc.ConvertFrom("#FFA280");
            }
            else if (importance.Value == 3)
            {
                imp_text.Text = "very important";
                BrushConverter bc = new BrushConverter();
                imp_text.Foreground = (Brush)bc.ConvertFrom("#FF7063");
            }
        }

        private void Add_button(object sender, RoutedEventArgs e)
        {

            if (name.Text != "" && category.SelectedItem != null)
            {
                if (sdate.SelectedDate != null && edate.SelectedDate == null)
                {
                    task = new Models.Task(name.Text, (int)importance.Value, sdate.SelectedDate.Value, category.SelectedItem as Category);
                    foreach(var x in subtasks)
                    {
                        task.SubTasks.Add(x);
                    }
                    DialogResult = true;
                }
                else if (sdate.SelectedDate != null && edate.SelectedDate != null)
                {
                    task = new Models.Task(name.Text, (int)importance.Value, sdate.SelectedDate.Value, edate.SelectedDate.Value, category.SelectedItem as Category);
                    foreach (var x in subtasks)
                    {
                        task.SubTasks.Add(x);
                    }
                    DialogResult = true;
                }
                else
                {
                    task = new Models.Task(name.Text, (int)importance.Value, category.SelectedItem as Category);
                    foreach (var x in subtasks)
                    {
                        task.SubTasks.Add(x);
                    }
                    DialogResult = true;
                }
            } else
            {
                MessageBox.Show("Incorrectly completed form.");
            }
        }

        private void Add_Subtask_Click(object sender, RoutedEventArgs e)
        {
            

            if (sdate.SelectedDate != null && edate.SelectedDate != null)
            {
                AddSubtask addSubtask = new AddSubtask(sdate.SelectedDate.Value, edate.SelectedDate.Value);
                if (addSubtask.ShowDialog() == true)
                {
                    subtasks.Add(addSubtask.subtask);
                    subtasks_list.Items.Refresh();
                }
            }
            else if (sdate.SelectedDate != null && edate.SelectedDate == null)
            {
                AddSubtask addSubtask = new AddSubtask(sdate.SelectedDate.Value);
                if (addSubtask.ShowDialog() == true)
                {
                    subtasks.Add(addSubtask.subtask);
                    subtasks_list.Items.Refresh();
                }
            }
            else
            {
                AddSubtask addSubtask = new AddSubtask();
                if (addSubtask.ShowDialog() == true)
                {
                    subtasks.Add(addSubtask.subtask);
                    subtasks_list.Items.Refresh();
                }
            }

        }
    }
}

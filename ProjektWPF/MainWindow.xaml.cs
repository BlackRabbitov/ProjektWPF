using ProjektWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ProjektWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ProjektWPF.Models.Task> tasks;
        public List<Category> categories;
        public List<Category> timeCategories;

        public bool shouldMinimalize = true;

        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        private WindowState m_storedWindowState = WindowState.Normal;

        private bool sortByCreationDate, sortByDate, sortByImportance;
        public MainWindow()
        {
            InitializeComponent();
            Init_ByTimeCategories();
            tasks = GetAllTasks();

            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = "ToDoer has been minimised. Click the tray icon to show.";
            m_notifyIcon.BalloonTipTitle = "ToDoer";
            m_notifyIcon.Text = "ToDoer";
            m_notifyIcon.Icon = new System.Drawing.Icon("../../Source/iconfinder-ring-4341316_120544.ico");
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);

            tasks = new List<Models.Task>();
            categories = new List<Category>();

            string fileName = "../../Data/ToDo.json";
            string jsonString = File.ReadAllText(fileName);
            categories = JsonSerializer.Deserialize<List<Category>>(jsonString);
            foreach(Category category in categories)
            {
                foreach(Models.Task task in category.Tasks)
                {
                    task.Category = category;
                    tasks.Add(task);
                }
            }
            this.basicSort();

            Category_ListBox.DataContext = categories;
            Tasks_ListBox.DataContext = tasks;
        }

        // Should work in background when minimalized
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MinHeight = this.ActualHeight;
            this.MinWidth = this.ActualWidth;
            this.MaxWidth = this.ActualWidth;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string fileName = "../../Data/ToDo.json";
            string jsonString = JsonSerializer.Serialize(categories);
            File.WriteAllText(fileName, jsonString);

            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(shouldMinimalize == true)
            {
                if (WindowState == WindowState.Minimized)
                {
                    Hide();
                    if (m_notifyIcon != null)
                        m_notifyIcon.ShowBalloonTip(2000);
                }
                else
                    m_storedWindowState = WindowState;
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CheckTrayIcon();
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = m_storedWindowState;
        }
        void CheckTrayIcon()
        {
            ShowTrayIcon(!IsVisible);
        }

        void ShowTrayIcon(bool show)
        {
            if (m_notifyIcon != null)
                m_notifyIcon.Visible = show;
        }

        // -- Should work in background when minimalized

        // Categories

        private void AddCategory_Button_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();

            if(categoryWindow.ShowDialog() == true)
            {
                categories.Add(new Category(categoryWindow.CategoryName.Text));
                Category_ListBox.Items.Refresh();
            }
        }

        private void EditCategory_Button_Click(object sender, RoutedEventArgs e)
        {
            Category categorySelected = Category_ListBox.SelectedItem as Category;
            if (categorySelected != null)
            {
                CategoryWindow categoryWindow = new CategoryWindow();
                categoryWindow.CategoryName.Text = categorySelected.Name;

                if (categoryWindow.ShowDialog() == true)
                {
                    categorySelected.Name = categoryWindow.CategoryName.Text;
                    Category_ListBox.Items.Refresh();
                }
            }
        }

        private void DeleteCategory_Button_Click(object sender, RoutedEventArgs e)
        {
            Category categorySelected = Category_ListBox.SelectedItem as Category;
            if(categorySelected != null)
            {
                categories.Remove(categories.Find(x => x.Name == categorySelected.Name));
                Category_ListBox.Items.Refresh();
            }
        }

        private void CategoriesTab_Button_Click(object sender, RoutedEventArgs e)
        {
            Category_ListBox.ItemsSource = categories;
            Category_ListBox.Items.Refresh();

            AddCategory_Button.IsEnabled = true;
            DeleteCategory_Button.IsEnabled = true;
            EditCategory_Button.IsEnabled = true;
        }

        private void ByTimeCategories_Button_Click(object sender, RoutedEventArgs e)
        {
            Category_ListBox.ItemsSource = timeCategories;
            Category_ListBox.Items.Refresh();

            AddCategory_Button.IsEnabled = false;
            DeleteCategory_Button.IsEnabled = false;
            EditCategory_Button.IsEnabled = false;
        }

        // -- Categories

        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.ShouldMinimalize_CheckBox.IsChecked = shouldMinimalize;
            
            if(settingsWindow.ShowDialog() == true)
            {
                shouldMinimalize = settingsWindow.ShouldMinimalize_CheckBox.IsChecked.Value;
            }
        }

        private void AddTask_Button_Click(object sender, RoutedEventArgs e)
        {
            AddTask addTask = new AddTask(categories);

            if (addTask.ShowDialog() == true)
            {
                tasks.Add(addTask.task);
                categories.Find(x => x.Name == addTask.task.Category.Name).Tasks.Add(addTask.task);
                Tasks_ListBox.Items.Refresh();
            }
        }

        private void Export_Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            string filename = dialog.SelectedPath;

            // Get the selected file name and display in a TextBox 
            if (filename != "")
            {
                // Open document 
                filename += "/ToDo.json";
                string fileToExportFromName = "../../Data/ToDo.json";
                File.Copy(fileToExportFromName, filename);
            }
        }

        private void Import_Button_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON Files (*.json)|*.json";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                string fileToInportIntoName = "../../Data/ToDo.json";
                string backupFileToInportIntoName = "../../Data/ToDo.json.bac";
                File.Replace(filename, fileToInportIntoName, backupFileToInportIntoName, false);
                
                string jsonString = File.ReadAllText(fileToInportIntoName);
                categories = JsonSerializer.Deserialize<List<Category>>(jsonString);
                tasks.Clear();

                foreach (Category category in categories)
                {
                    foreach (Models.Task task in category.Tasks)
                    {
                        task.Category = category;
                        tasks.Add(task);
                    }
                }
                this.basicSort();
            }
        }

        public void Init_ByTimeCategories()
        {
            timeCategories = new List<Category>();

            timeCategories.Add(new Category("In year"));
            timeCategories.Add(new Category("In month"));
            timeCategories.Add(new Category("In week"));
            timeCategories.Add(new Category("Tomorrow"));
            timeCategories.Add(new Category("Today"));
        }

        public List<Models.Task> GetAllTasks()
        {
            List<Models.Task> tasks = new List<Models.Task>();
            
            if(categories != null)
            {
                foreach (Category category in categories)
                {
                    foreach (Models.Task task in category.Tasks)
                    {
                        tasks.Add(task);
                    }
                }
            }

            return tasks;
        }

        private void Category_ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Category category = Category_ListBox.SelectedItem as Category;

            if(category != null)
            {
                tasks = category.Tasks;
                this.basicSort();
                Tasks_ListBox.ItemsSource = tasks;
                Tasks_ListBox.Items.Refresh();
            }
        }

        private void EditTask_Button_Click(object sender, RoutedEventArgs e)
        {
            Models.Task selectedTask = Tasks_ListBox.SelectedItem as Models.Task;

            if (selectedTask != null)
            {
                AddTask taskWindow = new AddTask(categories);
                taskWindow.name.Text = selectedTask.Name;
                taskWindow.importance.Value = selectedTask.Importance;
                if (selectedTask.StartDate != new DateTime())
                {
                    taskWindow.check.IsChecked = true;
                    taskWindow.sdate.SelectedDate = selectedTask.StartDate;
                    taskWindow.edate.SelectedDate = selectedTask.EndDate;
                }else
                {
                    taskWindow.check.IsChecked = false;
                }

                taskWindow.category.SelectedItem = selectedTask.Category;
                taskWindow.add.Content = "Modify";

                if (taskWindow.ShowDialog() == true)
                {
                    selectedTask.Name = taskWindow.name.Text;
                    selectedTask.Importance = (int)taskWindow.importance.Value;
                    if (taskWindow.check.IsChecked != false)
                    {
                        selectedTask.StartDate = taskWindow.sdate.SelectedDate.Value;
                        selectedTask.EndDate = taskWindow.edate.SelectedDate.Value;
                    }
                    selectedTask.Category = taskWindow.category.SelectedItem as Category;
                    Tasks_ListBox.Items.Refresh();
                }
            }
        }

        private void DeleteTask_Button_Click(object sender, RoutedEventArgs e)
        {
            Models.Task taskSelected = Tasks_ListBox.SelectedItem as Models.Task;
            if (taskSelected != null)
            {
                tasks.Remove(tasks.Find(x => x.Name == taskSelected.Name));
                categories.Find(x => x == taskSelected.Category).Tasks.Remove(taskSelected);
                Tasks_ListBox.Items.Refresh();
            }
        }

        private void Tasks_ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Models.Task taskSelected = Tasks_ListBox.SelectedItem as Models.Task;
            if (taskSelected != null)
            {
                TaskDetails taskDetails = new TaskDetails();
                taskDetails.SetSourceTask(taskSelected);
                if (taskDetails.ShowDialog() == true)
                {

                }
            }
        }

        private void OrderByDate_Button_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            tasks = (List<Models.Task>)Tasks_ListBox.ItemsSource;
            if (sortByDate)
                OrderByDate_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            else
                OrderByDate_Button.Background = Brushes.Green;
            OrderByImportance_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            sortByImportance = false;
            OrderByCreationDate_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            sortByCreationDate = false;
            sortByDate = !sortByDate;

            this.basicSort();

            Tasks_ListBox.ItemsSource = tasks;
            Tasks_ListBox.Items.Refresh();
        }

        private void OrderByImportance_Button_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            tasks = (List<Models.Task>)Tasks_ListBox.ItemsSource;
            if (sortByImportance)
                OrderByImportance_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            else
                OrderByImportance_Button.Background = Brushes.Green;
            OrderByCreationDate_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            sortByCreationDate = false;
            OrderByDate_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            sortByDate = false;
            sortByImportance = !sortByImportance;

            this.basicSort();

            Tasks_ListBox.ItemsSource = tasks;
            Tasks_ListBox.Items.Refresh();
        }

        private void OrderByCreationDate_Button_Click(object sender, RoutedEventArgs e)
        {
            var bc = new BrushConverter();

            tasks = (List<Models.Task>)Tasks_ListBox.ItemsSource;
            if (sortByCreationDate)
                OrderByCreationDate_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            else
                OrderByCreationDate_Button.Background = Brushes.Green;
            OrderByImportance_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            sortByImportance = false;
            OrderByDate_Button.Background = (Brush)bc.ConvertFrom("#E6E6E6");
            sortByDate = false;
            sortByCreationDate = !sortByCreationDate;

            this.basicSort();

            Tasks_ListBox.ItemsSource = tasks;
            Tasks_ListBox.Items.Refresh();
        }

        private void sortByCreationDateF()
        {
            //tasks.Sort((x, y) => DateTime.Compare(x.DateOfCreation, y.DateOfCreation));
        }

        private void sortByImportanceF()
        {
            //tasks.Sort((x, y) => x.Importance - y.Importance);
            tasks = tasks.OrderBy(x => x.Importance).ToList();
        }

        private void sortByDateF()
        {
            //tasks.Sort((x, y) => DateTime.Compare(x.StartDate, y.StartDate));
            tasks = tasks.OrderBy(x => x.StartDate).ToList();
        }

        private void basicSort()
        {
            if (sortByCreationDate)
                this.sortByCreationDateF();
            else if (sortByImportance)
                this.sortByImportanceF();
            else if (sortByDate)
                this.sortByDateF();
            else
                tasks = tasks.OrderBy(x => x.Name).ToList();
        }
    }
}

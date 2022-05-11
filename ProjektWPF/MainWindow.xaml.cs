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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        private WindowState m_storedWindowState = WindowState.Normal;
        public MainWindow()
        {
            InitializeComponent();
            m_notifyIcon = new System.Windows.Forms.NotifyIcon();
            m_notifyIcon.BalloonTipText = "ToDoer has been minimised. Click the tray icon to show.";
            m_notifyIcon.BalloonTipTitle = "ToDoer";
            m_notifyIcon.Text = "ToDoer";
            m_notifyIcon.Icon = new System.Drawing.Icon("../../Source/iconfinder-ring-4341316_120544.ico");
            m_notifyIcon.Click += new EventHandler(m_notifyIcon_Click);
        }
        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            this.MinHeight = this.ActualHeight;
            this.MinWidth = this.ActualWidth;
            this.MaxWidth = this.ActualWidth;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_notifyIcon.Dispose();
            m_notifyIcon = null;
        }

        private void Window_StateChanged(object sender, EventArgs e)
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

        private void AddCategory_Button_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.Show();
        }

        private void EditCategory_Button_Click(object sender, RoutedEventArgs e)
        {
            CategoryWindow categoryWindow = new CategoryWindow();
            categoryWindow.CategoryName.Text = "Test category name";
            categoryWindow.Show();
        }
    }
}

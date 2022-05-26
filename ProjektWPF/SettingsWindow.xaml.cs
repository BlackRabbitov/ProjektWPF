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
    /// Logika interakcji dla klasy SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Settings_Listbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((Settings_Listbox.SelectedItem as ListBoxItem).Name == "General_Settings_Button")
            {
                Display_Layout_Settings_StackPanel.Visibility = Visibility.Hidden;
                Display_Layout_Settings_StackPanel.IsEnabled = false;
                Display_General_Settings_StackPanel.Visibility = Visibility.Visible;
                Display_General_Settings_StackPanel.IsEnabled = true;
            } 
            else if ((Settings_Listbox.SelectedItem as ListBoxItem).Name == "Layout_Settings_Button")
            {
                Display_Layout_Settings_StackPanel.Visibility = Visibility.Visible;
                Display_Layout_Settings_StackPanel.IsEnabled = true;
                Display_General_Settings_StackPanel.Visibility = Visibility.Hidden;
                Display_General_Settings_StackPanel.IsEnabled = false;
            }
        }
    }
}

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
using TaskProjectWPF.Models;
using TaskProjectWPF.Pages;
using TaskProjectWPF.Service;

namespace TaskProjectWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();  
            App.contextProject = DataInit.Projects.First();
            MainFrame.Navigate(new TestDragDrop());
            Refresh();
        }

        private void Refresh()
        {
            LBProject.ItemsSource = DataInit.Projects
                .OrderByDescending(t=>t.Id)
                .Take(5);
        }

        private void BCalendar_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CalendarPage());
        }

        private void BTasks_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TaskPage());
        }

        private void BDashboard_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DashboardPage());
        }

        private void LBProject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(LBProject.SelectedItem is Project project)
            {
                App.contextProject = project;
                
                
            }
        }
    }
}

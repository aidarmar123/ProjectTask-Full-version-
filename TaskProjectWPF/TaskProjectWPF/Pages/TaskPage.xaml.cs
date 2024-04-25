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
using TaskProjectWPF.Service;
using TaskProjectWPF.Windows;

namespace TaskProjectWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {

        List<Models.Task> tasks = DataInit.Tasks.Where(t=>t.ProjectId==App.contextProject.Id).ToList();
        Models.Task taskContext;
        public TaskPage()
        {
            InitializeComponent();

            Refresh();
        }

        private void Refresh()
        {
            LVTasks.ItemsSource = tasks.OrderBy(t => t.StatusId == 3 && t.Deadline>DateTime.Now)
                .OrderBy(t=>t.StatusId==2 && t.Deadline>DateTime.Now)
                .OrderByDescending(t=>t.StatusId == 3)
                .OrderByDescending(t=>t.StatusId==2).ThenBy(t=>t.StatusId)
                ;
        }

        private void TBTaskName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var taskName = TBTaskName.Text;
            if (!string.IsNullOrEmpty(taskName))
            {
                tasks = DataInit.Tasks.Where(x => x.FullSearch.Contains(taskName) && x.ProjectId == App.contextProject.Id).ToList();
            }
            else
            {
                tasks = DataInit.Tasks.Where(t => t.ProjectId == App.contextProject.Id).ToList();
            }
            Refresh();
        }

        private void LVTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            if (Content.ColumnDefinitions.Count >= 2)
            {
                Content.ColumnDefinitions.Remove(Content.ColumnDefinitions.First());
            }

            if (LVTasks.SelectedItem is Models.Task task)
            {
                
                Content.ColumnDefinitions.Add(new ColumnDefinition());
                BContentTask.Visibility = Visibility.Visible;
                taskContext = task;
                BContentTask.DataContext = taskContext;
                return;
            }
            


        }

        private async void Badd_Click(object sender, RoutedEventArgs e)
        {
            await NetManager.PutData(taskContext, "api/EditTasks");
            Content.ColumnDefinitions.Remove(Content.ColumnDefinitions.First());
            BContentTask.Visibility = Visibility.Collapsed;
            taskContext = null;
            Refresh();
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            var response = await NetManager.DeletData($"api/RemoveTasks/{taskContext.Id}");
            Content.ColumnDefinitions.Remove(Content.ColumnDefinitions.First());
            BContentTask.Visibility = Visibility.Collapsed;
            taskContext = null;
            Refresh();
        }

        private void BInstall_Click(object sender, RoutedEventArgs e)
        {
            new InstallTaskList().Show();
        }
    }
}

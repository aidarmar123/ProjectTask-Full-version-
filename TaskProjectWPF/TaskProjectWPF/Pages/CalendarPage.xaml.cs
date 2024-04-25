
using LibraryTaskProjects.Service;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
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
using TaskProjectWPF.Service;
using Excel = Microsoft.Office.Interop.Excel;

namespace TaskProjectWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }

        public CalendarPage()
        {
            InitializeComponent();
            DateStart = new DateTime(2023, 7, 1);
            DateEnd = new DateTime(2023, 7, 5);
            RefreshContent();
            SPHeader.DataContext = this;
            CreateGrid();
            RefreshGridContent();

          
        }

       
        private void RefreshDate(int countDay)
        {
            DateStart=DateStart.AddDays(countDay);
            DateEnd=DateEnd.AddDays(countDay);
            SPHeader.DataContext = null;
            SPHeader.DataContext = this;
            
            RefreshGridContent();
            RefreshContent();
        }
        private void RefreshContent()
        {
            var tasks = DataInit.Tasks.Where(t => t.StartActualTimeNotNull >= DateStart 
                                                  && t.StartActualTimeNotNull <= DateEnd 
                                                  && t.ProjectId==App.contextProject.Id)
                                                  .ToList();
            foreach (var task in tasks)
            {
                //Создание Задачи
                try
                {
                    var _stackPanelTask = new StackPanel();
                    _stackPanelTask.Margin = new Thickness(3);
                    _stackPanelTask.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(251, 206, 245));

                    var taskName = new TextBlock();
                    taskName.Text = task.ShortTitle;
                    taskName.Foreground = System.Windows.Media.Brushes.Gray;
                    _stackPanelTask.Children.Add(taskName);

                    Grid.SetColumn(_stackPanelTask, task.StartActualTimeNotNull.Day - DateStart.Day + 1);

                    var taskTime = task.FinishActualTime.Value;
                    Grid.SetRowSpan(_stackPanelTask, taskTime.Hour - task.StartActualTimeNotNull.Hour);

                    Grid.SetRow(_stackPanelTask, 24 - task.StartActualTimeNotNull.Hour);
                    Content.Children.Add(_stackPanelTask);




                    if (task.PreviousTaskId != null)
                    {
                        var prevoisTask = DataInit.Tasks.FirstOrDefault(t => t.Id == task.PreviousTaskId);
                        Image img = new Image();
                        img.Width = 5000;
                        img.Height = 200;
                        var rowNow = 24 - task.StartActualTimeNotNull.Hour;
                        var rowPrevious = 24 - prevoisTask.StartActualTimeNotNull.Hour;
                        Grid.SetColumn(img, task.StartActualTimeNotNull.Day - DateStart.Day + 1);

                        if (rowPrevious > rowNow)
                        {
                            Grid.SetRow(img, 24 - task.StartActualTimeNotNull.Hour + 1);
                            Grid.SetRowSpan(img, rowPrevious - rowNow);

                            img.Source = new BitmapImage(new Uri("/Images/ArrowUp.jpeg", UriKind.Relative));
                        }
                        else
                        {
                            Grid.SetRow(img, rowPrevious);
                            Grid.SetRowSpan(img, rowNow - rowPrevious + 2);
                            img.Source = new BitmapImage(new Uri("/Images/ArrowDown.jpeg", UriKind.Relative));
                        }



                        img.Width = 100;
                        img.Height = 100;

                        Content.Children.Add(img);
                    }

                }
                catch
                {

                }


            }
            var line = new Line();
            line.X1 = 1;
            line.Y1 = 1;
            line.X2 = 10000;
            line.Y2 = 1;
            line.Stroke = Brushes.Blue;
            Grid.SetColumnSpan(line, 10);
            Grid.SetRow(line, 24 - DateTime.Now.Hour);
            Content.Children.Add(line);
            
           
        }

        private void CreateGrid()
        {
            
            for (int i = 24; i >= 0; i--)
            {
                Content.RowDefinitions.Add(new RowDefinition());
             
            }
            

            for (int i = DateStart.Day - 1; i < DateEnd.Day + 1; i++)
            {
                Content.ColumnDefinitions.Add(new ColumnDefinition());
               
            }
        }

        private void RefreshGridContent()
        {
            Content.Children.Clear();
            for (int i = 24; i >= 0; i--)
            {
                
                if (24 - i != 0)
                {
                    var time = new TextBlock();
                    if (i == 0)
                        time.Text = $"00:00";
                    else
                        time.Text = $"{24 - i}:00";
                    Grid.SetColumn(time, 0);
                    Grid.SetRow(time, i);
                    Content.Children.Add(time);
                }
            }
            for (int i = 0; i <= DateEnd.Day - DateStart.Day+1; i++)
            {
                if (i != 0)
                {

                    var day = new TextBlock();
                    var date = DateStart.AddDays(i-1);
                    var month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Month);
                    day.Text = $"{date.Day} {month}";
                    day.HorizontalAlignment = HorizontalAlignment.Center;
                    Grid.SetColumn(day, i);
                    Grid.SetRow(day, 24);
                    Content.Children.Add(day);
                }
            }
            RefreshContent();
        }

        private void BPlus_Click(object sender, RoutedEventArgs e)
        {
            RefreshDate(1);
        }
        private void BBack_Click(object sender, RoutedEventArgs e)
        {
            RefreshDate(-1);
        }

        private async void BAddData_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog() { Filter=".xlsx | *.xlsx"};
            if (openDialog.ShowDialog().GetValueOrDefault())
            {
                var file = openDialog.FileName;
                var excelApp = new Excel.Application();
                Excel.Workbook workbook = excelApp.Workbooks.Open(file);
                Excel.Worksheet worksheet = workbook.Worksheets[1];
                Excel.Range excelRange = worksheet.UsedRange;
                int rows = excelRange.Rows.Count;
                for (int i = 2; i <= rows; i++)
                {
                    
                        var task = new Models.Task();
                        task.ProjectId = App.contextProject.Id;
                        task.ShortTitle = excelRange.Cells[i, 1].Value.ToString();
                        task.FullTitle = excelRange.Cells[i, 2].Text;
                        task.Decription = excelRange.Cells[i, 3].Text;
                        task.ExecutiveEmployeeId = int.Parse(excelRange.Cells[i, 4].Value.ToString());

                        task.StatusId = int.Parse(excelRange.Cells[i, 7].Text);
                        task.CreatedTime = Convert.ToDateTime(excelRange.Cells[i, 8].Value.ToString());
                        task.UpdatedTime = Convert.ToDateTime(excelRange.Cells[i, 9].Value.ToString());
                        //task.DeletedTime = Convert.ToDateTime(excelRange.Cells[i, 10].Value.ToString());
                        task.StartActualTime = Convert.ToDateTime(excelRange.Cells[i, 14].Value.ToString());
                        task.FinishActualTime = Convert.ToDateTime(excelRange.Cells[i, 15].Value.ToString());
                        task.Deadline = Convert.ToDateTime(excelRange.Cells[i, 12].Value.ToString());
                        await NetManager.PostData(task, "api/Tasks");
                    
                }
            }
        }
    }
}

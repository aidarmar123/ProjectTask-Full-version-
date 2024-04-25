using LiveCharts.Defaults;
using LiveCharts;
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
using TaskProjectWPF.Service;

namespace TaskProjectWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        Border draggedElement;

        public ChartValues<HeatPoint> Values { get; set; }
        public List<string> Days { get; set; }
        public string[] Months { get; set; }
        public DashboardPage()
        {
            InitializeComponent();
            RefreshDiagram();
        }

        
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            draggedElement = sender as Border;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var dropElement = sender as Border;

            

            AlgorithmMoving(draggedElement, dropElement);
        }

        private void AlgorithmMoving(Border dragElement, Border dropElement)
        {
            var sizeEl = Grid.GetColumnSpan(dragElement);

            Content.Children.Remove(dragElement);
            Content.Children.Insert(Convert.ToInt32(dropElement.Tag),dragElement);
            
        }
        private void RefreshDiagram()
        {

            Values = new ChartValues<HeatPoint>();

            Days = new List<string>
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
            Months = new[]
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            };
            
            
            var tasks = DataInit.Tasks;

            for (int i = 0; i < 365; i++)
            {

                var taskInDay = tasks.Where(t => t.FinishActualTime != null).ToList();
                taskInDay = taskInDay.Where(t => t.FinishActualTime.Value.DayOfYear == i).ToList();
                if (taskInDay.Count > 0)
                {
                    var dayOfWeek = taskInDay[0].FinishActualTime.Value.DayOfWeek;
                    Values.Add(new HeatPoint(i / 7, Days.IndexOf(dayOfWeek.ToString()), taskInDay.Count));
                }
            }

            DataContext = this;
        }
    }
}
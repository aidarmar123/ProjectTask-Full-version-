using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using LiveCharts.Defaults;
using LiveCharts;
using LiveCharts.Wpf;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using TaskProjectWPF.Service;

namespace TaskProjectWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestDragDrop.xaml
    /// </summary>
    public partial class TestDragDrop : Page
    {

        public TestDragDrop()
        {
            InitializeComponent();
    
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
            Refresh();
            DataContext = this;
        }

        private void Refresh()
        {
            var tasks = DataInit.Tasks;
            
            for (int i = 0; i < 365; i++)
            {
                
                var taskInDay = tasks.Where(t => t.FinishActualTime != null).ToList();
                taskInDay = taskInDay.Where(t=> t.FinishActualTime.Value.DayOfYear == i).ToList();
                if (taskInDay.Count > 0)
                {
                    var dayOfWeek = taskInDay[0].FinishActualTime.Value.DayOfWeek;
                    Values.Add(new HeatPoint(i/7, Days.IndexOf(dayOfWeek.ToString()), taskInDay.Count));
                }
            }
            
           
        }

        public ChartValues<HeatPoint> Values { get; set; }
        public List<string> Days { get; set; }
        public string[] Months { get; set; }

        

    }
}

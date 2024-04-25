using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Serialization;
using TaskProjectWPF.Service;
using LibraryTaskProjects;

namespace TaskProjectWPF.Windows
{
    /// <summary>
    /// Логика взаимодействия для InstallTaskList.xaml
    /// </summary>
    public partial class InstallTaskList : Window
    {
        public InstallTaskList()
        {
            InitializeComponent();
        }

        private void BJson_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog() { Filter = ".json | *.json" };
            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                var task = DataInit.Tasks.Where(t => t.ProjectId == App.contextProject.Id).ToList();
                var jsonData = JsonConvert.SerializeObject(task);
                File.WriteAllText(saveDialog.FileName, jsonData);
                
            }
        }

        private void BXml_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog() { Filter = ".xml | *.xml" };
            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                var task = DataInit.Tasks.Where(t => t.ProjectId == App.contextProject.Id).ToList();
                using (var stream = new FileStream(saveDialog.FileName, FileMode.Create))
                {
                    var xmlSerialzer = new XmlSerializer(typeof(List<Models.Task>));
                    xmlSerialzer.Serialize(stream, task.ToList());
                }
            }
        }

        private void BTxt_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog() { Filter = ".txt | *.txt" };
            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                var task = DataInit.Tasks.Where(t => t.ProjectId == App.contextProject.Id).ToList();
                var data = task.First();

                string dataStr = data.ToString();
                File.WriteAllText(saveDialog.FileName, dataStr);
            }
        }

        private void BExcel_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog() { Filter=""};
            if (saveDialog.ShowDialog().GetValueOrDefault())
            {
                var task = DataInit.Tasks.First();

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskProjectWPF.Models;
using TaskProjectWPF.Service;

namespace TaskProjectWPF
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static Project contextProject;
        public  App()
        {
            DataInit.DataInitAllList();
        }
    }
}

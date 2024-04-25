using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProjectWPF.Models;

namespace TaskProjectWPF.Service
{
    public class DataInit
    {
        public static List<Models.TaskStatus> Statuses { get; set; }
        public static List<Models.Task> Tasks { get; set; }
        public static List<Project> Projects { get; set; }
        public static List<Employee> Employees { get; set; }
        public static List<Role> Roles { get; set; }
        public static List<Portfolio> Portfolios { get; set; }


        public static async void DataInitAllList()
        {
            Portfolios = await RefreshData<List<Portfolio>>("api/Portfolios");
            Statuses = await RefreshData<List<Models.TaskStatus>>("api/TaskStatus");
            Tasks = await RefreshData<List<Models.Task>>("api/Tasks");
            Projects = await RefreshData<List<Project>>("api/Projects");
            Roles = await RefreshData<List<Role>>("api/Roles");
            Employees = await RefreshData<List<Employee>>("api/Employees");
           
        }

        public static async Task<T> RefreshData<T>(string path)
        {
            var response = await NetManager.GetResponse(path);
            var content=  await NetManager.ParseData<T>(response);
            return content;
        }
    }
}

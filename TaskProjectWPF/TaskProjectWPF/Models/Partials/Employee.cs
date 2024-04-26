using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProjectWPF.Service;

namespace TaskProjectWPF.Models
{
    partial class Employee
    {
        public string FullName
        {
            get
            {
                return $"{firstName} {middleName} {lastName}";
            }
        }
        public int CountTask
        {
            get
            {
                return DataInit.Tasks.Where(t=>
                 t.ProjectId == App.contextProject.Id
                && t.ExecutiveEmployeeId == Id).Count();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProjectWPF.Models
{
    partial class Project
    {
        public string Abbr
        {
            get
            {
                return $"{ShortTittle[0]}{ShortTittle[ShortTittle.Length-1]}";
            }
        }
    }  
}

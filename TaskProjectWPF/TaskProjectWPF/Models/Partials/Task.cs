using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProjectWPF.Models
{
    partial class Task
    {
        private DateTime RoundTime(DateTime time)
        {
            if (time.Hour < 12)
            {

            }
            return time;
        }
        public DateTime StartActualTimeNotNull
        {
            get
            {
                if(StartActualTime == null)
                    return DateTime.MinValue;
                else
                    return StartActualTime.Value;
            }
        }
        public string FullSearch
        {
            get
            {
                return FullTitle + ShortTitle + Decription;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProjectWPF.Service;

namespace TaskProjectWPF.Models
{
    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            
            this.Task = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string FullTittle { get; set; }
        public string ShortTittle { get; set; }
        public byte[] Icon { get; set; }
        public System.DateTime CreadetTime { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
        public System.DateTime StartScheduleDate { get; set; }
        public System.DateTime FinishScheduledDate { get; set; }
        public string Desription { get; set; }
        public int CreatorEmployeedId { get; set; }
        public int ResponsibleEmployeeId { get; set; }

        public virtual Employee CreaterEmployee
        {
            get
            {
                return DataInit.Employees.FirstOrDefault(e => e.Id == CreatorEmployeedId);
            }
            set
            {
                CreatorEmployeedId = value.Id;
            }
        }
        public virtual Employee esponsableEmployee
        {
            get
            {
                return DataInit.Employees.FirstOrDefault(e => e.Id == ResponsibleEmployeeId);
            }
            set
            {
                ResponsibleEmployeeId = value.Id;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        
        public virtual ICollection<Task> Task { get; set; }
    }
}

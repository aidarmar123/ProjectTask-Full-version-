using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskProjectWPF.Service;

namespace TaskProjectWPF.Models
{
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.Comment = new HashSet<Comment>();
            this.Project = new HashSet<Project>();
            this.Project1 = new HashSet<Project>();
            this.Task = new HashSet<Task>();
            this.Task1 = new HashSet<Task>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public System.DateTime created_time { get; set; }
        public Nullable<System.DateTime> update_time { get; set; }
        public Nullable<System.DateTime> deletd_time { get; set; }
        public Nullable<System.DateTime> last_enter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment { get; set; }
        
        public virtual Role Role
        {
            get
            {
                return DataInit.Roles.FirstOrDefault(r=>r.Id==RoleId);
            }
            set
            {
                RoleId = value.Id;
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Project> Project1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Task { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Task1 { get; set; }
    }
}

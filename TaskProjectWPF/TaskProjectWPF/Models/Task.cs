using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using TaskProjectWPF.Service;

namespace TaskProjectWPF.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public Nullable<int> ProjectId { get; set; }
        public string FullTitle { get; set; }
        public string ShortTitle { get; set; }
        public string Decription { get; set; }
        public int ExecutiveEmployeeId { get; set; }
        public int StatusId { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public Nullable<System.DateTime> UpdatedTime { get; set; }
        public Nullable<System.DateTime> DeletedTime { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public System.DateTime? StartActualTime { get; set; }
        public System.DateTime? FinishActualTime { get; set; } 
        public int? PreviousTaskId { get; set; }
        public Nullable<int> CommentId { get; set; }
        public Nullable<int> LinkId { get; set; }
        public Nullable<int> OldStatusId { get; set; }
        public Nullable<System.DateTime> DateRefactorStatus { get; set; }
        public Nullable<int> CreaterId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public virtual Comment Comment { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public virtual Employee Employee
        {
            get
            {
                return DataInit.Employees.FirstOrDefault(e=>e.Id==CreaterId);
            }
            set
            {
                CreaterId = value.Id;
            }
        }

        [XmlIgnore]
        [JsonIgnore]
        public virtual Employee Employee1 { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public virtual LinkTask LinkTask { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public virtual Project Project { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public virtual TaskStatus TaskStatus
        {
            get
            {
                return DataInit.Statuses.FirstOrDefault(ts => ts.Id == StatusId);
            }
            set
            {
                StatusId = value.Id;
            }
        }
        [XmlIgnore]
        [JsonIgnore]
        public virtual TaskStatus OldTaskStatus
        {
            get
            {
                return DataInit.Statuses.FirstOrDefault(ts => ts.Id == OldStatusId);
            }
            set
            {
                OldStatusId = value.Id;
            }
        }
       
    }
}

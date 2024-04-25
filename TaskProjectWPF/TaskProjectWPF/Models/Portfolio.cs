using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace TaskProjectWPF.Models
{
    using System;
    using System.Collections.Generic;
    using TaskProjectWPF.Models;

    public partial class Portfolio
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Project Project { get; set; }
    }
}


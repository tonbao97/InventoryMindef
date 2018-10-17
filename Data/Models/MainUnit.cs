using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class MainUnit : BasicAttr
    {
        [DisplayName(@"Main Unit")]
        public string Name { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}

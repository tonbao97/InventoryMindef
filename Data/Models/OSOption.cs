using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OSOption : BasicAttr
    {
        [DisplayName(@"OS")]
        public string Name { get; set; }
        public virtual ICollection<ComputerDetails> Computers { get; set; }
    }
}

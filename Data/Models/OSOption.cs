using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class OSOption : BasicAttr
    {
        public virtual ICollection<ComputerDetails> Computers { get; set; }
    }
}

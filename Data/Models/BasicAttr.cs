using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public abstract partial class BasicAttr: BaseEntity
    {
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
    }
}

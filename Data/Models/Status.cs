using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Status: BasicAttr
    {
        [DisplayName(@"Status")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public Status()
        {
            this.CreatedDate = DateTime.Now;
        }
        public virtual ICollection<Item> Items { get; set; }

    }
}

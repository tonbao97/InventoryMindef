using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Brand: BasicAttr
    {
        public DateTime CreatedDate { get; set; }

        public Brand()
        {
            this.CreatedDate = DateTime.Now;
        }
        public virtual ICollection<Equipment> Equipments { get; set; }

    }
}

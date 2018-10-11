using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Category: BasicAttr
    {
        public DateTime CreatedDate { get; set; }

        public Category()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual EquipmentType EquipmentType { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }

    }
}

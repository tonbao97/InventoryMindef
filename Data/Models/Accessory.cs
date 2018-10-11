using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Accessory:BaseEntity
    {
        public int EquipmentID { get; set; }
        public int AccessoryID { get; set; }
        public string Note { get; set; }

        public virtual Equipment Equipment { get; set; }
        public virtual Equipment Accessory_ { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class EquipmentType: BasicAttr
    {
        public DateTime CreatedDate { get; set; }

        public EquipmentType()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual ICollection<Category> Categories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class EquipmentType: BasicAttr
    {
        [DisplayName(@"EquipmentType")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public EquipmentType()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual ICollection<Category> Categories { get; set; }
    }
}

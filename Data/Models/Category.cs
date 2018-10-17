using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Category: BasicAttr
    {
        [DisplayName(@"Category")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int EquipmentTypeID { get; set; }

        public Category()
        {
            this.CreatedDate = DateTime.Now;
        }
        [ForeignKey("EquipmentTypeID")]
        public virtual EquipmentType EquipmentTypes { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }

    }
}

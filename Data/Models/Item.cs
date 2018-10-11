using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Item: BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public int QRCode { get; set; }
        public int EquipmentID { get; set; }
        public int StatusID { get; set; }

        public Item()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual Equipment Equipment { get; set; }
        public virtual Status Status { get; set; }

    }
}

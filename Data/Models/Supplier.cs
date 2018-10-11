using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Supplier: BasicAttr
    {
        public DateTime CreatedDate { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Supplier()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual ICollection<Equipment> Equipments { get; set; }

    }
}

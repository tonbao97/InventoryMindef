using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Equipment: BasicAttr
    {
        public DateTime CreatedDate { get; set; }
        public int CategoryID { get; set; }
        public int Quantity { get; set; }
        public int BrandID { get; set; }
        public int SupplierID { get; set; }
        public double Price { get; set; }
        public Equipment()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual Brand Brand { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}

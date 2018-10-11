using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Ownership:BaseEntity
    {
        public int StaffID { get; set; }
        public int ItemID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Note { get; set; }

        public Ownership()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual Staff Staff { get; set; }
        public virtual Item Item { get; set; }
    }
}

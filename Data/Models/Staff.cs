using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Staff: BasicAttr
    {
        public DateTime CreatedDate { get; set; }
        public string Designation { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public int DepartmentID { get; set; }
        public int SubUnitID { get; set; }
        public int UnitID { get; set; }
        public int MainUnitID { get; set; }
        public Staff()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual Department Department { get; set; }
        public virtual SubUnit SubUnit { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual MainUnit MainUnit { get; set; }
    }
}

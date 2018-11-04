using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ComputerDetails: BaseEntity
    {
        public int ComputerID { get; set; }
        public int? RamOptionID { get; set; }
        public int? CPUOptionID { get; set; }
        public int? HDDOptionID { get; set; }
        public int? OSOptionID { get; set; }
        public int? VGAOptionID { get; set; }

        public virtual RamOption Ram { get; set; }
        public virtual CPUOption CPU { get; set; }
        public virtual HDDOption HDD { get; set; }
        public virtual OSOption OS { get; set; }
        public virtual VGAOption VGA { get; set; }
        [ForeignKey("ComputerID")]
        public virtual Equipment Computer { get; set; }


    }
}

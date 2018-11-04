using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class SearchingByTypeModel
    {
        public int BrandID { get; set; }
        public string Brand { get; set; }
        public List<EquipmentWithQuantity> List { get; set; }
    }

    public class EquipmentWithQuantity
    {
        public int EquipmentID { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
    }
}
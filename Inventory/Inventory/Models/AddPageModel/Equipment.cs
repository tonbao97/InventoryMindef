using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models
{
    class Equipment
    {
        public string EquipmentId { get; set; }
        public string SerialNo { get; set; }
        public string ItemCategory { get; set; }
        public string ItemType { get; set; }
        public string Location { get; set; }
    }
}

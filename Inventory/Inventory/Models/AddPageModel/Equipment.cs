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
        public string UserId { get; set; } // temp property is for app only

        public string DeliveryOrderNo { get; set; }
        public string DeliveryDate { get; set; }
        public string SupplierName { get; set; }
        public int CategoryID { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
    }
}

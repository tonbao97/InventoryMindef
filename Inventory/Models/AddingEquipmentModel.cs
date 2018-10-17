using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class AddingEquipmentModel
    {
        public string DeliveryOrderNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string SupplierName { get; set; }
        public int CategoryID { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }
    }
}
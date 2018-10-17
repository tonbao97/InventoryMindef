using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models
{
    public class Item
    {
        public string DeliveryOrderNo { get; set; }
        public string DeliveryDate { get; set; }
        public string SupplierName { get; set; }
        public int CategoryID { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public string Picture { get; set; }

        public Item(string deliveryOrderNo, string deliveryDate, string supplierName, int categoryID, string brandName, string model, int quantity, string picture)
        {
            DeliveryOrderNo = deliveryOrderNo;
            DeliveryDate = deliveryDate;
            SupplierName = supplierName;
            CategoryID = categoryID + 1;
            BrandName = brandName;
            Model = model;
            Quantity = quantity;
            Picture = picture;
        }

        public override string ToString()
        {
            return string.Format($"{DeliveryOrderNo}  {DeliveryDate}  {SupplierName}  {CategoryID} {BrandName} {Model} {Quantity} {Picture} ");
        }
    }
}

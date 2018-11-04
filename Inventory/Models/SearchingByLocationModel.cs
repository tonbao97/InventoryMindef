using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class SearchingByLocationModel
    {
        public int EquipmentTypeId { get; set; }
        public string EquipmentType { get; set; }
        public List<CategoryWithQuantity> List { get; set; }
    }

    public class CategoryWithQuantity
    {
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public List<ItemByCategoryModel> ListItem { get; set; }
    }

    public class CategoryWithQuantityTemp
    {
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public List<ItemByCategoryModel> ListItem { get; set; }

    }
}
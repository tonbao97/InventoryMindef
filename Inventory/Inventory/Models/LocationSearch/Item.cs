using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.LocationSearch
{
    class Item
    {
        public Item(int categoryId, string category, int quantity, string detail, List<IssuedItem> listItem)
        {
            CategoryId = categoryId;
            Category = category;
            Quantity = quantity;
            Detail = category + "("+quantity+")";
            ListItem = listItem;
        }

        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Detail { get; set; }
        public List<IssuedItem> ListItem { get; set; }
    }
}

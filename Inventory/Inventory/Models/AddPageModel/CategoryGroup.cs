using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models
{
    class CategoryGroup : List<Equipment>
    {
        public string EquipmentCategory { get; set; }
        public string IndexTitle { get; set; }

        public CategoryGroup(string category, string indexTitle)
        {
            EquipmentCategory = category;
            IndexTitle = indexTitle;
        }
    }
}

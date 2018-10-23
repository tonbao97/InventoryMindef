using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models
{
    class CategoryGroup : List<Equipment>
    {
        public string SearchCategory { get; set; }
        public string IndexTitle { get; set; }

        public CategoryGroup(string searchCategory, string indexTitle) // Consider changing this category to abstract so that we can apply location/user/item option automatically
        {
            SearchCategory = searchCategory;
            IndexTitle = indexTitle;
        }
    }
}

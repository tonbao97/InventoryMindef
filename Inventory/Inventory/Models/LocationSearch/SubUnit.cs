using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.LocationSearch
{
    class SubUnit
    {
        public SubUnit(int id, string name)
        {
            this.id = id;
            Name = name;
        }

        public int id { get; set; }
        public string Name { get; set; }
    }
}

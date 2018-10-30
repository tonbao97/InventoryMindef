using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.LocationSearch
{
    class Type
    {
        public int EquipmentTypeId { get; set; }
        public string EquipmentType { get; set; }
        public List<Item> List { get; set; }
        public Type(int equipmentTypeId, string equipmentType, List<Item> itemsList)
        {
            EquipmentTypeId = equipmentTypeId;
            EquipmentType = equipmentType;
            List = itemsList;
        }
    }
}

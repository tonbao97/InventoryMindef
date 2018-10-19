using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.AddPageModel
{
    class Category
    {
        public Category(string name, string createdDate, int equipmentTypeID, string equipmentTypes, string equipments, string description, string note, bool isActive, int id)
        {
            Name = name;
            CreatedDate = createdDate;
            EquipmentTypeID = equipmentTypeID;
            EquipmentTypes = equipmentTypes;
            Equipments = equipments;
            Description = description;
            Note = note;
            IsActive = isActive;
            Id = id;
        }

        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public int EquipmentTypeID { get; set; }
        public string EquipmentTypes { get; set; }
        public string Equipments { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }
}

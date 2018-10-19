using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.AddPageModel
{
    public class EquipmentTypes
    {
        public EquipmentTypes(string name, string createdDate, string categories, string description, string note, bool isActive, int id)
        {
            Name = name;
            CreatedDate = createdDate;
            Categories = categories;
            Description = description;
            Note = note;
            IsActive = isActive;
            Id = id;
        }

        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string Categories { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.LocationSearch
{
    public class IssuedItem
    {
        public IssuedItem(int id, string issuedTo, string designation, string mainUnit, string unit, string subUnit, string department, string brand, string model, string issuedDate)
        {
            Id = id;
            IssuedTo = issuedTo;
            Designation = designation;
            MainUnit = mainUnit;
            Unit = unit;
            SubUnit = subUnit;
            Department = department;
            Brand = brand;
            Model = model;
            IssuedDate = issuedDate;
            Detail = "Id:" + id.ToString() + "- Brand:" + brand + "- Model:" + model;
        }

        public int Id { get; set; }
         public string IssuedTo { get; set; }
         public string Designation { get; set; }
         public string MainUnit { get; set; }
         public string Unit { get; set; }
         public string SubUnit { get; set; }
         public string Department { get; set; }
         public string Brand { get; set; }
         public string Model { get; set; }
         public string IssuedDate { get; set; }
        public string Detail { get; set; }
    }
}

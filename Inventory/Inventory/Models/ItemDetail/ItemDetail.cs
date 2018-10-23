using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.ItemDetail
{
    class ItemDetail
    {
        public ItemDetail(string mainUnit, string unit, string subUnit, string department, string staffName, string identityNo, string contactNo, string equipmentType, string category, string model, string serialNo, string brand, string status, string oS, string processor, string rAM, string hDD, string vGA, string picture)
        {
            MainUnit = mainUnit;
            Unit = unit;
            SubUnit = subUnit;
            Department = department;
            StaffName = staffName;
            IdentityNo = identityNo;
            ContactNo = contactNo;
            EquipmentType = equipmentType;
            Category = category;
            Model = model;
            SerialNo = serialNo;
            Brand = brand;
            Status = status;
            OS = oS;
            Processor = processor;
            RAM = rAM;
            HDD = hDD;
            VGA = vGA;
            Picture = picture;
        }

        public string MainUnit { get; set; }
        public string Unit { get; set; }
        public string SubUnit { get; set; }
        public string Department { get; set; }
        public string StaffName { get; set; }
        public string IdentityNo { get; set; }
        public string ContactNo { get; set; }
        public string EquipmentType { get; set; }
        public string Category { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; }
        public string OS { get; set; }
        public string Processor { get; set; }
        public string RAM { get; set; }
        public string HDD { get; set; }
        public string VGA { get; set; }
        public string Picture { get; set; }
    }
}

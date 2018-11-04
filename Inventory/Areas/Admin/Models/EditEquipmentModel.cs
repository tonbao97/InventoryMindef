using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Inventory.Areas.Admin.Models
{
    public class EditEquipmentModel
    {
        public int Id { get; set; }
        //public Equipment Equipment { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public int Quantity { get; set; }
        public int BrandID { get; set; }
        public int DeliveryPackageID { get; set; }
        public int PictureID { get; set; }
        public double Price { get; set; }
        public bool IsConfirmed { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        //public ComputerDetails computerDetails { get; set; }
        public int? ComputerID { get; set; }
        public int? RamOptionID { get; set; }
        public int? CPUOptionID { get; set; }
        public int? HDDOptionID { get; set; }
        public int? OSOptionID { get; set; }
        public int? VGAOptionID { get; set; }

        public int EquipmentTypeId { get; set; }
        public override string ToString()
        {
            PropertyInfo[] _PropertyInfos = null;
            if (_PropertyInfos == null)
                _PropertyInfos = this.GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            return sb.ToString();
        }
    }
}
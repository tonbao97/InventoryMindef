using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Inventory.Models
{
    public class ItemInfoModel
    {
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
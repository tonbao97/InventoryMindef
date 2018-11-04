using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Inventory.Models
{
    public class AddingEquipmentModel
    {
        public string DeliveryOrderNo { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string SupplierName { get; set; }
        public int CategoryID { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
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
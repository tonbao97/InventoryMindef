using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Inventory.Areas.Admin.Models
{
    public class IssueModel
    {
        public int ItemID { get; set; }
        public int EquipmentID { get; set; }
        public int StatusID { get; set; }
        public string SerialNo { get; set; }
        public int? StaffID { get; set; }
        public string Note { get; set; }
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
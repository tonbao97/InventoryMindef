using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inventory.Models
{
    public class SearchingByUserModel
    {
        public int StaffId { get; set; }
        public string Fullname { get; set; }
        public string Designation { get; set; }
        public string IdentityNo { get; set; }
        public string MainUnit { get; set; }
        public string Unit { get; set; }
        public string SubUnit { get; set; }
        public string Department { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public string Email { get; set; }
        public List<ShortItemModel> IssuedItems { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.UserSearch
{
    public class User
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
        public string Detail {
            get { return Fullname + " (" + IssuedItems.Count + ")"; }
            set { }
        }
        public List<IssuedItem> IssuedItems { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models
{
    class User
    {
     

        string username { get; set; }
        string password { get; set; }
        string grant_type { get; set; } = "password";

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models
{
    class Token
    {
        public Token(string access_token, string token_type, int expires_in, string userName, string issuedDate, string expiresDate)
        {
            this.access_token = access_token;
            this.token_type = token_type;
            this.expires_in = expires_in;
            this.userName = userName;
            this.issuedDate = issuedDate;
            this.expiresDate = expiresDate;
        }

        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; }
        public string issuedDate { get; set; }
        public string expiresDate { get; set; }


    }
}

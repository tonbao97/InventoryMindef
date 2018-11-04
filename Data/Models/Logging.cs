using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Logging:BaseEntity
    {
        public DateTime Time { get; set; }
        public string IP { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Params { get; set; }
        public string UserEmail { get; set; }
        public string Url { get; set; }
        public string RawUrl { get; set; }
    }
}

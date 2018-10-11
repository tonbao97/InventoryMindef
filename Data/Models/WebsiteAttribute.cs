using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class WebsiteAttribute: BasicAttr
    {
        public string Content { get; set; }
        public string Url { get; set; }
        public string ControlType { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsPublished { get; set; }
    }
}

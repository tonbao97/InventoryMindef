using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class IssueType: BasicAttr
    {
        public DateTime CreatedDate { get; set; }

        public IssueType()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Issue: BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public int IssueTypeID { get; set; }
        public int ItemID { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Event { get; set; }
        public string Remarks { get; set; }
        public DateTime CollectedDate { get; set; }
        public string CollectedBy { get; set; }
        public string IssueBy { get; set; }
        public DateTime ReturnedDate { get; set; }
        public string ReturnedBy { get; set; }
        public string ReceivedBy { get; set; }

        public Issue()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual IssueType IssueType { get; set; }
        public virtual Item Item { get; set; }

    }
}

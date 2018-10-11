using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class DeliveryPackage: BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public int SupplierID { get; set; }
        public string DeliveryOrderNo { get; set; }
        public DateTime DODate { get; set; }
        public string ProcurementType { get; set; }
        public string ProcurementFileNumber { get; set; }
        public string RequisitionNumber { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public DateTime PODate { get; set; }
        public string Receiver { get; set; }
        public DateTime ReceivedDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public string Appointment { get; set; }
        public string DropTo { get; set; }
        public string Remarks { get; set; }
        public double TotalAmt { get; set; }
        public string Type { get; set; }
        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string UnitPrice { get; set; }
        public string WarrantyExp { get; set; }
        public bool Completed { get; set; }


        public DeliveryPackage()
        {
            this.CreatedDate = DateTime.Now;
        }

        public virtual Supplier Supplier { get; set; }
    }
}

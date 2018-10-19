using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.AddPageModel
{
    public class Supplier
    {
        public Supplier(string name, string createdDate, string contactPerson, string mobileNumber, string telephoneNumber, string fax, string email, string address, string deliveryPackages, string description, string note, bool isActive, int id)
        {
            Name = name;
            CreatedDate = createdDate;
            ContactPerson = contactPerson;
            MobileNumber = mobileNumber;
            TelephoneNumber = telephoneNumber;
            Fax = fax;
            Email = email;
            Address = address;
            DeliveryPackages = deliveryPackages;
            Description = description;
            Note = note;
            IsActive = isActive;
            Id = id;
        }

        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string ContactPerson { get; set; }
        public string MobileNumber { get; set; }
        public string TelephoneNumber { get; set; }
        public  string Fax { get; set; }
        public string Email { get; set; }
        public  string Address { get; set; }
        public string DeliveryPackages { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }

    }
}

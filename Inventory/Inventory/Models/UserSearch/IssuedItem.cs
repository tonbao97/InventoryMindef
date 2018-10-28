using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Models.UserSearch
{
    class IssuedItem
    {


        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Detail { get; private set; }
        public IssuedItem(int id, string brand, string model)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Detail = id.ToString() + "- " + brand + "- " + model;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Brand: BasicAttr
    {
        [DisplayName(@"Brand")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        public Brand()
        {
            this.CreatedDate = DateTime.Now;
            this.IsActive = true;
        }
        public virtual ICollection<Equipment> Equipments { get; set; }

        //public override string ToString()
        //{

        //    string result = this.GetType().Name + "{";
        //    foreach(var field in this.GetType().GetFields())
        //    {
        //        result = result + field.Name + "=" + field.GetValue(this) + ", ";
        //    }
        //    result = result + "}";
        //    return result /*"Brand{Name = " + (Name == null? "Null" : Name) + "}"*/;
        //}
        
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Data;
using Data.Models;
using Inventory.Models;

namespace Inventory.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [Authorize]
    public class ItemsController : ApiController
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: api/Items
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }
        [ResponseType(typeof(ItemInfoModel))]
        [Route("api/getiteminfo/{Id}")]
        public IHttpActionResult GetItemInfo(int Id)
        {
            ItemInfoModel model = new ItemInfoModel();
            Item item = db.Items.Find(Id);
            if (item == null)
            {   
                return NotFound();
            }
            Equipment equipment = item.Equipment;
            ComputerDetails computerDetails = db.ComputerDetailss.Where(p => p.ComputerID == equipment.Id).FirstOrDefault();
            Staff staff = db.Ownerships.Where(p => p.ItemID == Id && p.IsActive).FirstOrDefault().Staff;
            model.MainUnit = staff.MainUnit.Name;
            model.Unit = staff.Unit.Name;
            model.SubUnit = staff.SubUnit.Name;
            model.Department = staff.Department.Name;
            model.StaffName = staff.Name;
            model.IdentityNo = staff.IdentityNo;
            model.ContactNo = staff.MobileNumber;
            model.EquipmentType = equipment.Category.EquipmentTypes.Name;
            model.Category = equipment.Category.Name;
            model.Model = equipment.Name;
            model.SerialNo = item.SerialNo;
            model.Brand = equipment.Brand.Name;
            model.Status = item.Status.Name;
            model.OS = computerDetails.OS.Name;
            model.RAM = computerDetails.Ram.Name;
            model.Processor = computerDetails.CPU.Name;
            model.HDD = computerDetails.HDD.Name;
            model.VGA = computerDetails.VGA.Name;
            return Ok(model);
        }

        // GET: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult GetItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/Items/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutItem(int id, Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            db.Entry(item).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Items
        [ResponseType(typeof(Item))]
        public IHttpActionResult PostItem(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [ResponseType(typeof(Item))]
        public IHttpActionResult DeleteItem(int id)
        {
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return Ok(item);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int id)
        {
            return db.Items.Count(e => e.Id == id) > 0;
        }
    }
}
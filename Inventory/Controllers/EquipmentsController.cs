using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;
using Data;
using Data.Models;
using Inventory.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Service;

namespace Inventory.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    [Authorize]
    public class EquipmentsController : ApiController
    {
        private InventoryEntities db = new InventoryEntities();
        private ApplicationUserManager _userManager;

        public EquipmentsController()
        {
        }

        public EquipmentsController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: api/Equipments
        public IQueryable<Equipment> GetEquipments()
        {
            return db.Equipments;
        }

        public IQueryable<BrandModel> GetBrands()
        {
            
            return db.Brands.Select(p => new BrandModel() { Id = p.Id, Name = p.Name });
        }

        public IQueryable<SupplierModel> GetSuppliers()
        {
            return db.Suppliers.Select(p => new SupplierModel() { Id = p.Id, Name = p.Name });
        }

        public IQueryable<EquipmentTypeModel> GetEquipmentTypes()
        {
            return db.EquipmentTypes.Select(p => new EquipmentTypeModel() { Id = p.Id, Name = p.Name });
        }

        public IQueryable<CategoryModel> GetCategories()
        {
            return db.Categories.Select(p => new CategoryModel() { Id = p.Id, Name = p.Name });
        }

        public IQueryable<Equipment> SearchEquipments()
        {
            return db.Equipments;
        }

        // GET: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult GetEquipment(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return Ok(equipment);
        }

        // PUT: api/Equipments/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEquipment(int id, Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != equipment.Id)
            {
                return BadRequest();
            }

            db.Entry(equipment).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
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

        // POST: api/Equipments
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult PostEquipment(Equipment equipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Equipments.Add(equipment);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = equipment.Id }, equipment);
        }

        public IHttpActionResult AddEquipment(AddingEquipmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Brand> list = db.Brands.ToList();
            Brand brand = list.Where(p => p.Name.ToUpper().Equals(model.BrandName.ToUpper())).FirstOrDefault();
            if (brand == null)
            {
                Brand newBrand = new Brand();
                newBrand.Name = model.BrandName;
                db.Brands.Add(newBrand);
                db.SaveChanges();
                brand = newBrand;
            }
            Supplier supplier = db.Suppliers.Where(p => p.Name.Equals(model.SupplierName)).FirstOrDefault();
            if (supplier == null)
            {
                Supplier newSupplier = new Supplier();
                newSupplier.Name = model.SupplierName;
                supplier = newSupplier;
                supplier.IsActive = true;
                db.Suppliers.Add(newSupplier);
                db.SaveChanges();

            }
            DeliveryPackage deliveryPackage = db.DeliveryPackages.Where(p => p.DeliveryOrderNo.ToUpper().Equals(model.DeliveryOrderNo.ToUpper())).FirstOrDefault();
            if (deliveryPackage == null)
            {
                DeliveryPackage newDP = new DeliveryPackage();
                newDP.DeliveryOrderNo = model.DeliveryOrderNo;
                newDP.DODate = model.DeliveryDate;
                newDP.SupplierID = supplier.Id;
                try
                {
                    newDP.ReceiverID = UserManager.FindById(User.Identity.GetUserId()).StaffID.Value;

                }
                catch (Exception)
                {

                    newDP.ReceiverID = 0;
                }
                newDP.IsActive = true;
                newDP.IsConfirmed = false;
                db.DeliveryPackages.Add(newDP);
                db.SaveChanges();
                deliveryPackage = newDP;
            }
            Picture picture = new Picture();
            picture.Url = model.Picture;
            picture.IsDeleted = false;
            db.Pictures.Add(picture);
            db.SaveChanges();
            Equipment equipment = new Equipment();
            equipment.Name = model.Model;
            equipment.CategoryID = model.CategoryID;
            equipment.BrandID = brand.Id;
            equipment.Quantity = model.Quantity;
            equipment.IsConfirmed = true;
            equipment.IsActive = true;
            equipment.DeliveryPackageID = deliveryPackage.Id;
            equipment.PictureID = picture.Id;
            db.Equipments.Add(equipment);
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/Equipments/5
        [ResponseType(typeof(Equipment))]
        public IHttpActionResult DeleteEquipment(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return NotFound();
            }

            db.Equipments.Remove(equipment);
            db.SaveChanges();

            return Ok(equipment);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EquipmentExists(int id)
        {
            return db.Equipments.Count(e => e.Id == id) > 0;
        }
    }
}
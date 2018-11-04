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
using Core.Common;
using Data;
using Data.Models;
using Inventory.CustomFilter;
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
        //public IQueryable<Equipment> GetEquipments()
        //{
        //    return db.Equipments;
        //}

        [Route("api/getunits")]
        [CustomFilters]
        public IQueryable<UnitModel> GetUnits()
        {

            return db.Units.Select(p => new UnitModel() { Id = p.Id, Name = p.Name });
        }

        [Route("api/getmainunits")]
        [CustomFilters]
        public IQueryable<MainUnitModel> GetMainUnits()
        {

            return db.MainUnits.Select(p => new MainUnitModel() { Id = p.Id, Name = p.Name });
        }

        [Route("api/getsubunits")]
        [CustomFilters]
        public IQueryable<SubUnitModel> GetSubUnits()
        {

            return db.SubUnits.Select(p => new SubUnitModel() { Id = p.Id, Name = p.Name });
        }

        [Route("api/getdepartments")]
        [CustomFilters]
        public IQueryable<DepartmentModel> GetDepartment()
        {

            return db.Departments.Select(p => new DepartmentModel() { Id = p.Id, Name = p.Name });
        }

        [CustomFilters]
        public IQueryable<BrandModel> GetBrands()
        {
            
            return db.Brands.Select(p => new BrandModel() { Id = p.Id, Name = p.Name });
        }

        [CustomFilters]
        public IQueryable<SupplierModel> GetSuppliers()
        {
            return db.Suppliers.Select(p => new SupplierModel() { Id = p.Id, Name = p.Name });
        }

        [CustomFilters]
        public IQueryable<EquipmentTypeModel> GetEquipmentTypes()
        {
            return db.EquipmentTypes.Select(p => new EquipmentTypeModel() { Id = p.Id, Name = p.Name });
        }

        [CustomFilters]
        public IQueryable<CategoryModel> GetCategories()
        {
            return db.Categories.Select(p => new CategoryModel() { Id = p.Id, Name = p.Name });
        }

        public IQueryable<Equipment> SearchEquipments()
        {
            return db.Equipments;
        }

        [HttpGet, HttpPost]
        [CustomFilters]
        [Route("api/categorybyequipmenttype/{Id}")]
        public IQueryable<CategoryWithQuantity> CategoryByEquipmentType(int Id)
        {
            if (db.EquipmentTypes.Find(Id) == null)
            {
                return null;
            }

            //issued items
            return db.Ownerships.Where(p => p.IsActive).Where(p => p.Item.Equipment.Category.EquipmentTypeID == Id)
                .GroupBy(p => p.Item.Equipment.Category)
                .Select(p => new CategoryWithQuantity()
                {
                    CategoryId = p.Key.Id,
                    Category = p.Key.Name,
                    Quantity = p.Count()
                });

            //not issued items
            //return db.Equipments.Where(p => p.Category.EquipmentTypeID == Id).GroupBy(p => p.Category)
            //    .Select(p => new CategoryWithQuantity() { CategoryId = p.Key.Id, Category = p.Key.Name, Quantity = p.Sum(k => k.Quantity) });
        }

        [HttpGet, HttpPost]
        [Route("api/equipmentbycategory/{Id}")]
        [CustomFilters]
        public IQueryable<SearchingByTypeModel> EquipmentByCategory(int Id)
        {
            if (db.Categories.Find(Id) == null)
            {
                return null;
            }
            return db.Equipments.Where(p => p.CategoryID == Id && p.IsActive).GroupBy(p => p.Brand)
                .Select(p => new SearchingByTypeModel()
                {
                    BrandID = p.Key.Id,
                    Brand = p.Key.Name,
                    List = p.ToList().Select(k => new EquipmentWithQuantity()
                    {
                        EquipmentID = k.Id,
                        Model = k.Name,
                        Quantity = k.Quantity
                    }).ToList()
                });
        }

        [Route("api/getitemsbycategory/{Id}")]
        [CustomFilters]
        public IQueryable<ItemByCategoryModel> GetItemsByCategory(int Id)
        {
            if (db.Categories.Find(Id) == null)
            {
                return null;
            }
            return db.Ownerships.Where(p => p.IsActive).Where(p => p.Item.Equipment.CategoryID == Id)
                .Select(p => new ItemByCategoryModel()
                {
                    Id = p.Item.Id,
                    Brand = p.Item.Equipment.Brand.Name,
                    Department = p.Staff.Department.Name,
                    Designation = p.Staff.Designation,
                    IssuedDate = p.CreatedDate,
                    IssuedTo = p.Staff.Name,
                    MainUnit = p.Staff.MainUnit.Name,
                    Model = p.Item.Equipment.Name,
                    SubUnit = p.Staff.SubUnit.Name,
                    Unit = p.Staff.Unit.Name
                });
        }

        [Route("api/searchbylocation")]
        [HttpPost, HttpGet]
        [CustomFilters]
        public IQueryable<SearchingByLocationModel> SearchByLocation(int? MainUnitId = null, int? UnitId = null, int? SubUnitId = null, int? DepartmentId = null)
        {
            var filter = db.Ownerships.Where(k => k.IsActive);
            if (MainUnitId != null)
            {
                filter = filter.Where(p => p.Staff.MainUnitID == MainUnitId);
            }
            if (SubUnitId != null)
            {
                filter = filter.Where(p => p.Staff.SubUnitID == SubUnitId);
            }
            if (UnitId != null)
            {
                filter = filter.Where(p => p.Staff.UnitID == UnitId);
            }
            if (DepartmentId != null)
            {
                filter = filter.Where(p => p.Staff.DepartmentID == DepartmentId);
            }
            var result = filter.GroupBy(p => p.Item.Equipment.Category)
                .Select(p => new CategoryWithQuantityTemp() {
                    Category = p.Key,
                    Quantity = p.Count(),
                    ListItem = p.ToList().Select(l => new ItemByCategoryModel() {
                        Id = l.Item.Id,
                        Brand = l.Item.Equipment.Brand.Name,
                        Department = l.Staff.Department.Name,
                        Designation = l.Staff.Designation,
                        IssuedDate = l.CreatedDate,
                        IssuedTo = l.Staff.Name,
                        MainUnit = l.Staff.MainUnit.Name,
                        Model = l.Item.Equipment.Name,
                        SubUnit = l.Staff.SubUnit.Name,
                        Unit = l.Staff.Unit.Name
                    }).ToList()
                })
                .GroupBy(p => p.Category.EquipmentTypes)
                .Select(p => new SearchingByLocationModel() { EquipmentTypeId = p.Key.Id, EquipmentType = p.Key.Name,
                    List  = p.ToList().Select(k => new CategoryWithQuantity() {
                        CategoryId = k.Category.Id,
                        Category = k.Category.Name,
                        Quantity = k.Quantity,
                        ListItem = k.ListItem
                    }).ToList()
                })
                
                ;
            return result;

        }

        [Route("api/searchbyuser")]
        [HttpGet]
        [CustomFilters]
        public IQueryable<SearchingByUserModel> SearchByUser(string keyword = "")
        {
            //var list = db.Staffs.Where(p => StringConvert.ConvertShortName(p.Name).Contains(shortKey)
            //|| StringConvert.ConvertShortName(p.IdentityNo).Contains(shortKey))
            var list = db.Staffs.Where(p => p.Name.Contains(keyword)
            || p.IdentityNo.Contains(keyword))
            .Select(p => new SearchingByUserModel() {Fullname = p.Name, Designation = p.Designation, Department = p.Department.Name,
            Email = p.Email, IdentityNo = p.IdentityNo, MainUnit = p.MainUnit.Name, MobileNumber = p.MobileNumber,
            SubUnit = p.SubUnit.Name, TelephoneNumber = p.TelephoneNumber, Unit = p.Unit.Name, StaffId = p.Id,
            IssuedItems = db.Ownerships.Where(l => l.StaffID == p.Id).
                    Select(k => new ShortItemModel() { Id = k.ItemID, Brand = k.Item.Equipment.Brand.Name, Model = k.Item.Equipment.Name }).ToList()
        });
            //foreach(var item in list)
            //{
            //    item.IssuedItems = db.Ownerships.Where(p => p.StaffID == item.StaffId).
            //        Select(k => new ShortItemModel() { Id = k.ItemID, Brand = k.Item.Equipment.Brand.Name, Model = k.Item.Equipment.Name }).ToList();
            //}
            return list;
        }
        //// GET: api/Equipments/5
        //[ResponseType(typeof(Equipment))]
        //public IHttpActionResult GetEquipment(int id)
        //{
        //    Equipment equipment = db.Equipments.Find(id);
        //    if (equipment == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(equipment);
        //}

        //// PUT: api/Equipments/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutEquipment(int id, Equipment equipment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != equipment.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(equipment).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EquipmentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Equipments
        //[ResponseType(typeof(Equipment))]
        //public IHttpActionResult PostEquipment(Equipment equipment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Equipments.Add(equipment);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = equipment.Id }, equipment);
        //}

        [CustomFilters]
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
        //[ResponseType(typeof(Equipment))]
        //public IHttpActionResult DeleteEquipment(int id)
        //{
        //    Equipment equipment = db.Equipments.Find(id);
        //    if (equipment == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Equipments.Remove(equipment);
        //    db.SaveChanges();

        //    return Ok(equipment);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool EquipmentExists(int id)
        //{
        //    return db.Equipments.Count(e => e.Id == id) > 0;
        //}
    }
}
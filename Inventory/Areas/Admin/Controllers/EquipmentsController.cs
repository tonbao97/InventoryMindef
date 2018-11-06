using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Models;
using Inventory.Areas.Admin.Models;
using Inventory.CustomFilter;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Service;

namespace Inventory.Areas.Admin.Controllers
{
    public class EquipmentsController : BaseController
    {
        private InventoryEntities db = new InventoryEntities();
        private IEquipmentTypeService equipmentTypeService;
        private IBrandService brandService;
        private ISupplierService supplierService;
        private IDeliveryPackageService deliveryPackageService;
        private IPictureService pictureService;
        private IEquipmentService equipmentService;
        private IOSOptionService OSOptionService;
        private IVGAOptionService VGAOptionService;
        private IHDDOptionService HDDOptionService;
        private ICPUOptionService CPUOptionService;
        private IRamOptionService RamOptionService;
        private ICategoryService categoryService;
        private IComputerDetailsService computerDetailsService;
        private IItemService itemService;
        private IOwnershipService ownershipService;
        private IStaffService staffService;
        private ApplicationUserManager _userManager;

        public EquipmentsController(IEquipmentTypeService equipmentTypeService, IBrandService brandService, 
            ISupplierService supplierService, IDeliveryPackageService deliveryPackageService, 
            IPictureService pictureService, IEquipmentService equipmentService, IOSOptionService oSOptionService, 
            IVGAOptionService vGAOptionService, IHDDOptionService hDDOptionService, ICPUOptionService cPUOptionService, 
            IRamOptionService ramOptionService, ICategoryService categoryService, IComputerDetailsService computerDetailsService,
            IItemService itemService, IOwnershipService ownershipService, IStaffService staffService)
        {
            this.equipmentTypeService = equipmentTypeService;
            this.brandService = brandService;
            this.supplierService = supplierService;
            this.deliveryPackageService = deliveryPackageService;
            this.pictureService = pictureService;
            this.equipmentService = equipmentService;
            OSOptionService = oSOptionService;
            VGAOptionService = vGAOptionService;
            HDDOptionService = hDDOptionService;
            CPUOptionService = cPUOptionService;
            RamOptionService = ramOptionService;
            this.categoryService = categoryService;
            this.computerDetailsService = computerDetailsService;
            this.itemService = itemService;
            this.ownershipService = ownershipService;
            this.staffService = staffService;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }




        // GET: Admin/Equipments
        [CustomFilters]
        public ActionResult Index()
        {
            ViewBag.EquipmentTypeId = equipmentTypeService.GetEquipmentTypes();
            var equipments = equipmentService.GetEquipments().AsQueryable().Include(e => e.Brand).Include(e => e.Category).Include(e => e.DeliveryPackage).Include(e => e.Picture);
            return View(equipments.ToList());
        }

        public ActionResult Issue(int Id)
        {
            ViewBag.StaffID = new SelectList(staffService.GetStaffs(), "Id", "Name");
            ViewBag.StatusID = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.EquipmentID = Id;
            return View();
        }
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Issue(IssueModel model)
        {
            if (ModelState.IsValid)
            {
                var equip = equipmentService.GetEquipmentById(model.EquipmentID);
                if (equip.Quantity < 1)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var item = new Item();
                item.EquipmentID = model.EquipmentID;
                item.SerialNo = model.SerialNo;
                item.StatusID = model.StatusID;
                itemService.CreateItem(item);
                var ownership = new Ownership();
                ownership.IsActive = true;
                ownership.ItemID = item.Id;
                ownership.StaffID = model.StaffID.Value;
                ownership.Note = model.Note;
                ownershipService.CreateOwnership(ownership);
                equip.Quantity -= 1;
                equipmentService.EditEquipment(equip);
                return RedirectToAction("Index");
            }
            ViewBag.StaffID = new SelectList(staffService.GetStaffs(), "Id", "Name");
            ViewBag.StatusID = new SelectList(db.Statuses, "Id", "Name");
            ViewBag.EquipmentID = model.EquipmentID;
            return View("Issue", model);

        }

        public ActionResult DeliveryPackage(int? Id)
        {
            ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name");
            if (Id != null)
            {
                ViewBag.DeliveryPackageID = new SelectList(deliveryPackageService.GetDeliveryPackages(), "Id", "DeliveryOrderNo", deliveryPackageService.GetDeliveryPackageById(Id.Value).Id);
            }
            else
            {
                ViewBag.DeliveryPackageID = new SelectList(deliveryPackageService.GetDeliveryPackages(), "Id", "DeliveryOrderNo");
            }
            return PartialView();
        }

        public ActionResult AddDeliveryPackage(DeliveryPackage deliveryPackage)
        {
            try
            {
                deliveryPackage.ReceiverID = UserManager.FindById(User.Identity.GetUserId()).StaffID.Value;

            }
            catch (Exception)
            {

                deliveryPackage.ReceiverID = 0;
            }
            deliveryPackage.IsActive = true;
            deliveryPackage.IsConfirmed = false;
            deliveryPackageService.CreateDeliveryPackage(deliveryPackage);
            return RedirectToAction("DeliveryPackage");
        }

        public ActionResult OSOption(int? Id)
        {
            if (Id != null)
            {
                ViewBag.OSOptionID = new SelectList(OSOptionService.GetOSOptions(), "Id", "Name", OSOptionService.GetOSOptionById(Id.Value).Id);
            }
            else
            {
                ViewBag.OSOptionID = new SelectList(OSOptionService.GetOSOptions(), "Id", "Name");
            }
            return PartialView();
        }
        [CustomFilters]
        public ActionResult AddOSOption(string OSOptionName)
        {
            if (OSOptionName != null || OSOptionName.Trim().Equals(""))
            {
                return RedirectToAction("OSOption");
            }
            if (OSOptionService.GetOSOptions().Where(p => p.Name.ToUpper().Equals(OSOptionName.ToUpper())).FirstOrDefault() == null)
            {
                OSOption os = new OSOption();
                os.Name = OSOptionName;
                os.IsActive = true;
                OSOptionService.CreateOSOption(os);
            } 
            return RedirectToAction("OSOption");
        }
        public ActionResult RamOption(int? Id)
        {
            if (Id != null)
            {
                ViewBag.RamOptionID = new SelectList(RamOptionService.GetRamOptions(), "Id", "Name", RamOptionService.GetRamOptionById(Id.Value).Id);
            }
            else
            {
                ViewBag.RamOptionID = new SelectList(RamOptionService.GetRamOptions(), "Id", "Name");
            }
            return PartialView();
        }
        [CustomFilters]
        public ActionResult AddRamOption(string RamOptionName)
        {
            if (RamOptionName ==  null || RamOptionName.Trim().Equals(""))
            {
                return RedirectToAction("OSOption");
            }
            if (RamOptionService.GetRamOptions().Where(p => p.Name.ToUpper().Equals(RamOptionName.ToUpper())).FirstOrDefault() == null)
            {
                RamOption Ram = new RamOption();
                Ram.Name = RamOptionName;
                Ram.IsActive = true;
                RamOptionService.CreateRamOption(Ram);
            }
            return RedirectToAction("RamOption");
        }
        public ActionResult HDDOption(int? Id)
        {
            if (Id != null)
            {
                ViewBag.HDDOptionID = new SelectList(HDDOptionService.GetHDDOptions(), "Id", "Name", HDDOptionService.GetHDDOptionById(Id.Value).Id);
            }
            else
            {
                ViewBag.HDDOptionID = new SelectList(HDDOptionService.GetHDDOptions(), "Id", "Name");
            }
            return PartialView();
        }
        [CustomFilters]
        public ActionResult AddHDDOption(string HDDOptionName)
        {
            if (HDDOptionName == null || HDDOptionName.Trim().Equals(""))
            {
                return RedirectToAction("OSOption");
            }
            if (HDDOptionService.GetHDDOptions().Where(p => p.Name.ToUpper().Equals(HDDOptionName.ToUpper())).FirstOrDefault() == null)
            {
                HDDOption HDD = new HDDOption();
                HDD.Name = HDDOptionName;
                HDD.IsActive = true;
                HDDOptionService.CreateHDDOption(HDD);
            }
            return RedirectToAction("HDDOption");
        }
        public ActionResult CPUOption(int? Id)
        {
            if (Id != null)
            {
                ViewBag.CPUOptionID = new SelectList(CPUOptionService.GetCPUOptions(), "Id", "Name", CPUOptionService.GetCPUOptionById(Id.Value).Id);
            }
            else
            {
                ViewBag.CPUOptionID = new SelectList(CPUOptionService.GetCPUOptions(), "Id", "Name");
            }
            return PartialView();
        }
        [CustomFilters]
        public ActionResult AddCPUOption(string CPUOptionName)
        {
            if (CPUOptionName == null || CPUOptionName.Trim().Equals(""))
            {
                return RedirectToAction("OSOption");
            }
            if (CPUOptionService.GetCPUOptions().Where(p => p.Name.ToUpper().Equals(CPUOptionName.ToUpper())).FirstOrDefault() == null)
            {
                CPUOption CPU = new CPUOption();
                CPU.Name = CPUOptionName;
                CPU.IsActive = true;
                CPUOptionService.CreateCPUOption(CPU);
            }
            return RedirectToAction("CPUOption");
        }
        public ActionResult VGAOption(int? Id)
        {
            if (Id != null)
            {
                ViewBag.VGAOptionID = new SelectList(VGAOptionService.GetVGAOptions(), "Id", "Name", VGAOptionService.GetVGAOptionById(Id.Value).Id);
            }
            else
            {
                ViewBag.VGAOptionID = new SelectList(VGAOptionService.GetVGAOptions(), "Id", "Name");
            }
            return PartialView();
        }
        [CustomFilters]
        public ActionResult AddVGAOption(string VGAOptionName)
        {
            if (VGAOptionName == null || VGAOptionName.Trim().Equals(""))
            {
                return RedirectToAction("OSOption");
            }
            if (VGAOptionService.GetVGAOptions().Where(p => p.Name.ToUpper().Equals(VGAOptionName.ToUpper())).FirstOrDefault() == null)
            {
                VGAOption VGA = new VGAOption();
                VGA.Name = VGAOptionName;
                VGA.IsActive = true;
                VGAOptionService.CreateVGAOption(VGA);
            }
            return RedirectToAction("VGAOption");
        }

        // GET: Admin/Equipments/Details/5
        [CustomFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = equipmentService.GetEquipmentById(id.Value);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Admin/Equipments/Create
        public ActionResult Create(int EquipmentTypeId)
        {
            var equipmentType = equipmentTypeService.GetEquipmentTypes().Where(p => p.Id == EquipmentTypeId).FirstOrDefault();
            ViewBag.EquipmentType = equipmentType;
            //ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name");
            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name");
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys().Where(p => p.EquipmentTypeID == EquipmentTypeId), "Id", "Name");
            ViewBag.DeliveryPackageID = new SelectList(deliveryPackageService.GetDeliveryPackages(), "Id", "DeliveryOrderNo");
            ViewBag.PictureID = new SelectList(pictureService.GetPictures(), "Id", "Binary");
            if (equipmentType.Name.Equals("Computer"))
            {
                ViewBag.OSOptionID = new SelectList(OSOptionService.GetOSOptions(), "Id", "Name");
                ViewBag.RamOptionID = new SelectList(RamOptionService.GetRamOptions(), "Id", "Name");
                ViewBag.HDDOptionID = new SelectList(HDDOptionService.GetHDDOptions(), "Id", "Name");
                ViewBag.CPUOptionID = new SelectList(CPUOptionService.GetCPUOptions(), "Id", "Name");
                ViewBag.VGAOptionID = new SelectList(VGAOptionService.GetVGAOptions(), "Id", "Name");
            }
            return View();
        }

        // POST: Admin/Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddingEquipmentModel model)
        {
            var equipmentType = equipmentTypeService.GetEquipmentTypes().Where(p => p.Id == model.EquipmentTypeId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var Equip = new Equipment();
                Equip.BrandID = model.BrandID;
                Equip.CategoryID = model.CategoryID;
                Equip.DeliveryPackageID = model.DeliveryPackageID;
                Equip.Description = model.Description;
                Equip.IsActive = model.IsActive;
                Equip.IsConfirmed = model.IsConfirmed;
                Equip.Name = model.Name;
                Equip.Note = model.Note;
                Equip.PictureID = model.PictureID;
                Equip.Price = model.Price;
                Equip.Quantity = model.Quantity;
                equipmentService.CreateEquipment(Equip);
                if (equipmentType.Name.Equals("Computer"))
                {
                    var ComDetails = new ComputerDetails();
                    ComDetails.ComputerID = Equip.Id;
                    ComDetails.HDDOptionID = model.HDDOptionID;
                    ComDetails.OSOptionID = model.OSOptionID;
                    ComDetails.RamOptionID = model.RamOptionID;
                    ComDetails.VGAOptionID = model.VGAOptionID;
                    ComDetails.CPUOptionID = model.CPUOptionID;
                    computerDetailsService.CreateComputerDetails(ComDetails);
                }
                
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentType = equipmentType;
            //ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name");
            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name", model.BrandID);
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys().Where(p => p.EquipmentTypeID == model.EquipmentTypeId), "Id", "Name", model.CategoryID);
            ViewBag.DeliveryPackageID = new SelectList(deliveryPackageService.GetDeliveryPackages(), "Id", "DeliveryOrderNo", model.DeliveryPackageID);
            ViewBag.PictureID = new SelectList(pictureService.GetPictures(), "Id", "Binary");
            if (equipmentType.Name.Equals("Computer"))
            {
                ViewBag.OSOptionID = new SelectList(OSOptionService.GetOSOptions(), "Id", "Name", model.OSOptionID);
                ViewBag.RamOptionID = new SelectList(RamOptionService.GetRamOptions(), "Id", "Name", model.RamOptionID);
                ViewBag.HDDOptionID = new SelectList(HDDOptionService.GetHDDOptions(), "Id", "Name", model.HDDOptionID);
                ViewBag.CPUOptionID = new SelectList(CPUOptionService.GetCPUOptions(), "Id", "Name", model.CPUOptionID);
                ViewBag.VGAOptionID = new SelectList(VGAOptionService.GetVGAOptions(), "Id", "Name", model.VGAOptionID);
            }
            return View(model);
        }

        // GET: Admin/Equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = equipmentService.GetEquipmentById(id.Value);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            var model = new EditEquipmentModel();
            model.BrandID = equipment.BrandID;
            model.CategoryID = equipment.CategoryID;
            model.DeliveryPackageID = equipment.DeliveryPackageID;
            model.Description = equipment.Description;
            model.EquipmentTypeId = equipment.Category.EquipmentTypeID;
            model.IsActive = equipment.IsActive;
            model.IsConfirmed = equipment.IsConfirmed;
            model.Name = equipment.Name;
            model.Note = equipment.Note;
            model.PictureID = equipment.PictureID;
            model.Price = equipment.Price;
            model.Quantity = equipment.Quantity;
            model.Id = equipment.Id;
            if (equipment.Category.EquipmentTypes.Name.Equals("Computer"))
            {
                var comDetails = computerDetailsService.GetComputerDetailss().Where(p => p.ComputerID == equipment.Id).FirstOrDefault();
                if (comDetails != null)
                {
                    ViewBag.OSOptionID = new SelectList(OSOptionService.GetOSOptions(), "Id", "Name", comDetails.OSOptionID);
                    ViewBag.RamOptionID = new SelectList(RamOptionService.GetRamOptions(), "Id", "Name", comDetails.RamOptionID);
                    ViewBag.HDDOptionID = new SelectList(HDDOptionService.GetHDDOptions(), "Id", "Name", comDetails.HDDOptionID);
                    ViewBag.CPUOptionID = new SelectList(CPUOptionService.GetCPUOptions(), "Id", "Name", comDetails.CPUOptionID);
                    ViewBag.VGAOptionID = new SelectList(VGAOptionService.GetVGAOptions(), "Id", "Name", comDetails.VGAOptionID);
                    model.CPUOptionID = comDetails.CPUOptionID;
                    model.HDDOptionID = comDetails.HDDOptionID;
                    model.OSOptionID = comDetails.OSOptionID;
                    model.RamOptionID = comDetails.RamOptionID;
                    model.VGAOptionID = comDetails.VGAOptionID;
                }
                else
                {
                    ViewBag.OSOptionID = new SelectList(OSOptionService.GetOSOptions(), "Id", "Name");
                    ViewBag.RamOptionID = new SelectList(RamOptionService.GetRamOptions(), "Id", "Name");
                    ViewBag.HDDOptionID = new SelectList(HDDOptionService.GetHDDOptions(), "Id", "Name");
                    ViewBag.CPUOptionID = new SelectList(CPUOptionService.GetCPUOptions(), "Id", "Name");
                    ViewBag.VGAOptionID = new SelectList(VGAOptionService.GetVGAOptions(), "Id", "Name");
                }
                
            }
            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name", equipment.BrandID);
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys(), "Id", "Name", equipment.CategoryID);
            ViewBag.DeliveryPackageID = new SelectList(deliveryPackageService.GetDeliveryPackages(), "Id", "DeliveryOrderNo", equipment.DeliveryPackageID);
            ViewBag.PictureID = new SelectList(pictureService.GetPictures(), "Id", "Binary", equipment.PictureID);
            ViewBag.EquipmentType = equipment.Category.EquipmentTypes;
            return View(model);
        }

        // POST: Admin/Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditEquipmentModel model)
        {
            var equipmentType = equipmentTypeService.GetEquipmentTypes().Where(p => p.Id == model.EquipmentTypeId).FirstOrDefault();
            if (ModelState.IsValid)
            {
                var Equip = new Equipment();
                Equip.Id = model.Id;
                Equip.BrandID = model.BrandID;
                Equip.CategoryID = model.CategoryID;
                Equip.DeliveryPackageID = model.DeliveryPackageID;
                Equip.Description = model.Description;
                Equip.IsActive = model.IsActive;
                Equip.IsConfirmed = model.IsConfirmed;
                Equip.Name = model.Name;
                Equip.Note = model.Note;
                Equip.PictureID = model.PictureID;
                Equip.Price = model.Price;
                Equip.Quantity = model.Quantity;
                equipmentService.EditEquipment(Equip);
                if (equipmentType.Name.Equals("Computer"))
                {
                    var ComDetails = computerDetailsService.GetComputerDetailss().Where(p => p.ComputerID == model.Id).FirstOrDefault();
                    if (ComDetails == null)
                    {
                        ComDetails = new ComputerDetails();
                        ComDetails.ComputerID = Equip.Id;
                        ComDetails.HDDOptionID = model.HDDOptionID;
                        ComDetails.OSOptionID = model.OSOptionID;
                        ComDetails.RamOptionID = model.RamOptionID;
                        ComDetails.VGAOptionID = model.VGAOptionID;
                        ComDetails.CPUOptionID = model.CPUOptionID;
                        computerDetailsService.CreateComputerDetails(ComDetails);
                    }
                    else
                    {
                        ComDetails.HDDOptionID = model.HDDOptionID;
                        ComDetails.OSOptionID = model.OSOptionID;
                        ComDetails.RamOptionID = model.RamOptionID;
                        ComDetails.VGAOptionID = model.VGAOptionID;
                        ComDetails.CPUOptionID = model.CPUOptionID;
                        computerDetailsService.EditComputerDetails(ComDetails);
                    }
                    
                }

                return RedirectToAction("Index");
            }

            ViewBag.EquipmentType = equipmentType;
            //ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name");
            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name", model.BrandID);
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys().Where(p => p.EquipmentTypeID == model.EquipmentTypeId), "Id", "Name", model.CategoryID);
            ViewBag.DeliveryPackageID = new SelectList(deliveryPackageService.GetDeliveryPackages(), "Id", "DeliveryOrderNo", model.DeliveryPackageID);
            ViewBag.PictureID = new SelectList(pictureService.GetPictures(), "Id", "Binary");
            if (equipmentType.Name.Equals("Computer"))
            {
                ViewBag.OSOptionID = new SelectList(OSOptionService.GetOSOptions(), "Id", "Name", model.OSOptionID);
                ViewBag.RamOptionID = new SelectList(RamOptionService.GetRamOptions(), "Id", "Name", model.RamOptionID);
                ViewBag.HDDOptionID = new SelectList(HDDOptionService.GetHDDOptions(), "Id", "Name", model.HDDOptionID);
                ViewBag.CPUOptionID = new SelectList(CPUOptionService.GetCPUOptions(), "Id", "Name", model.CPUOptionID);
                ViewBag.VGAOptionID = new SelectList(VGAOptionService.GetVGAOptions(), "Id", "Name", model.VGAOptionID);
            }
            return View(model);
        }

        // GET: Admin/Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = equipmentService.GetEquipmentById(id.Value);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Admin/Equipments/Delete/5
        [CustomFilters]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipmentService.DeleteEquipment(id);
            return RedirectToAction("Index");
        }

    }
}

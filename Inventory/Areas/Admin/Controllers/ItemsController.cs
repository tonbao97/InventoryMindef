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
using Service;

namespace Inventory.Areas.Admin.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private IItemService itemService;
        private IOwnershipService ownershipService;
        private IStaffService staffService;
        private IEquipmentService equipmentService;
        private IComputerDetailsService computerDetailsService;
        private IStatusService statusService;

        public ItemsController(IItemService itemService, IOwnershipService ownershipService, 
            IStaffService staffService, IEquipmentService equipmentService,
            IComputerDetailsService computerDetailsService, IStatusService statusService)
        {
            this.itemService = itemService;
            this.ownershipService = ownershipService;
            this.staffService = staffService;
            this.equipmentService = equipmentService;
            this.computerDetailsService = computerDetailsService;
            this.statusService = statusService;
        }



        // GET: Admin/Items
        [CustomFilters]
        public ActionResult Index()
        {
            var items = ownershipService.GetOwnerships().Where(p => p.IsActive).Select(p => new ItemsModel() {
                Id = p.ItemID,
                Category = p.Item.Equipment.Category.Name,
                Brand = p.Item.Equipment.Brand.Name,
                Model = p.Item.Equipment.Name,
                SerialNo = p.Item.SerialNo,
                Status = p.Item.Status.Name,
                IssuedTo = p.Staff.Name,
                Unit = p.Staff.Unit == null ? null : p.Staff.Unit.Name,
                CreatedDate = p.CreatedDate
            });
            return View(items.ToList());
        }

        // GET: Admin/Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ownerships = ownershipService.GetOwnerships().Where(p => p.IsActive && p.ItemID == id).FirstOrDefault();
            var item = itemService.GetItemById(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }
            var itemRe = new ItemsModel() {
                Id = item.Id,
                Brand = item.Equipment.Brand.Name,
                Category = item.Equipment.Category.Name,
                CreatedDate = ownerships.CreatedDate,
                EquipmentType = item.Equipment.Category.EquipmentTypes.Name,
                Model = item.Equipment.Name,
                Picture = item.Equipment.Picture.Url,
                SerialNo = item.SerialNo,
                Status = item.Status.Name,
            };
            var comDetails = computerDetailsService.GetComputerDetailss().Where(p => p.ComputerID == ownerships.Item.EquipmentID).FirstOrDefault();
            if (comDetails != null)
            {
                itemRe.HDD = comDetails.HDD == null ? null : comDetails.HDD.Name;
                itemRe.OS = comDetails.OS == null ? null : comDetails.OS.Name;
                itemRe.RAM = comDetails.Ram == null ? null : comDetails.Ram.Name;
                itemRe.VGA = comDetails.VGA == null ? null : comDetails.VGA.Name;
                itemRe.Processor = comDetails.CPU == null ? null : comDetails.CPU.Name;

            }
            if (ownerships != null)
            {
                var staff = ownerships.Staff;
                if (staff != null)
                {
                    itemRe.ContactNo = staff.MobileNumber;
                    itemRe.Department = staff.Department == null ? null : staff.Department.Name;
                    itemRe.IssuedTo = staff.Name;
                    itemRe.MainUnit = staff.MainUnit == null ? null : staff.MainUnit.Name;
                    itemRe.SubUnit = staff.SubUnit == null ? null : staff.SubUnit.Name;
                    itemRe.Unit = staff.Unit == null ? null : staff.Unit.Name;
                }
            }
            

            return View(itemRe);
        }

        // GET: Admin/Items/Create
        //public ActionResult Create()
        //{
        //    ViewBag.EquipmentID = new SelectList(db.Equipments, "Id", "Name");
        //    ViewBag.StatusID = new SelectList(db.Statuses, "Id", "Name");
        //    return View();
        //}

        // POST: Admin/Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,QRCode,EquipmentID,StatusID,SerialNo")] Item item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Items.Add(item);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EquipmentID = new SelectList(db.Equipments, "Id", "Name", item.EquipmentID);
        //    ViewBag.StatusID = new SelectList(db.Statuses, "Id", "Name", item.StatusID);
        //    return View(item);
        //}

        // GET: Admin/Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = itemService.GetItemById(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentID = new SelectList(equipmentService.GetEquipments(), "Id", "Name", item.EquipmentID);
            ViewBag.StatusID = new SelectList(statusService.GetStatuss(), "Id", "Name", item.StatusID);
            return View(item);
        }

        // POST: Admin/Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentID,StatusID,SerialNo")] Item item)
        {
            if (ModelState.IsValid)
            {
                itemService.EditItem(item);
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentID = new SelectList(equipmentService.GetEquipments(), "Id", "Name", item.EquipmentID);
            ViewBag.StatusID = new SelectList(statusService.GetStatuss(), "Id", "Name", item.StatusID);
            return View(item);
        }



        // GET: Admin/Items/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Item item = db.Items.Find(id);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(item);
        //}

        // POST: Admin/Items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Item item = db.Items.Find(id);
        //    db.Items.Remove(item);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

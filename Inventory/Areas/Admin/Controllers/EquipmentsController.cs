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

namespace Inventory.Areas.Admin.Controllers
{
    public class EquipmentsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Admin/Equipments
        public ActionResult Index()
        {
            var equipments = db.Equipments.Include(e => e.Brand).Include(e => e.Category).Include(e => e.DeliveryPackage);
            return View(equipments.ToList());
        }

        // GET: Admin/Equipments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // GET: Admin/Equipments/Create
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            ViewBag.DeliveryPackageID = new SelectList(db.DeliveryPackages, "Id", "DeliveryOrderNo");
            return View();
        }

        // POST: Admin/Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedDate,CategoryID,Quantity,BrandID,DeliveryPackageID,Price,IsConfirmed,Description,Note,IsActive")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Equipments.Add(equipment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name", equipment.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", equipment.CategoryID);
            ViewBag.DeliveryPackageID = new SelectList(db.DeliveryPackages, "Id", "DeliveryOrderNo", equipment.DeliveryPackageID);
            return View(equipment);
        }

        // GET: Admin/Equipments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name", equipment.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", equipment.CategoryID);
            ViewBag.DeliveryPackageID = new SelectList(db.DeliveryPackages, "Id", "DeliveryOrderNo", equipment.DeliveryPackageID);
            return View(equipment);
        }

        // POST: Admin/Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedDate,CategoryID,Quantity,BrandID,DeliveryPackageID,Price,IsConfirmed,Description,Note,IsActive")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equipment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(db.Brands, "Id", "Name", equipment.BrandID);
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", equipment.CategoryID);
            ViewBag.DeliveryPackageID = new SelectList(db.DeliveryPackages, "Id", "DeliveryOrderNo", equipment.DeliveryPackageID);
            return View(equipment);
        }

        // GET: Admin/Equipments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equipment equipment = db.Equipments.Find(id);
            if (equipment == null)
            {
                return HttpNotFound();
            }
            return View(equipment);
        }

        // POST: Admin/Equipments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equipment equipment = db.Equipments.Find(id);
            db.Equipments.Remove(equipment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

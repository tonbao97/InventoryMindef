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
    public class StaffsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Admin/Staffs
        public ActionResult Index()
        {
            var staffs = db.Staffs.Include(s => s.Department).Include(s => s.MainUnit).Include(s => s.SubUnit).Include(s => s.Unit);
            return View(staffs.ToList());
        }

        // GET: Admin/Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Admin/Staffs/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name");
            ViewBag.MainUnitID = new SelectList(db.MainUnits, "Id", "Name");
            ViewBag.SubUnitID = new SelectList(db.SubUnits, "Id", "Name");
            ViewBag.UnitID = new SelectList(db.Units, "Id", "Name");
            return View();
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,IdentityNo,Designation,MobileNumber,TelephoneNumber,DepartmentID,SubUnitID,UnitID,MainUnitID,Description,Note,IsActive")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name", staff.DepartmentID);
            ViewBag.MainUnitID = new SelectList(db.MainUnits, "Id", "Name", staff.MainUnitID);
            ViewBag.SubUnitID = new SelectList(db.SubUnits, "Id", "Name", staff.SubUnitID);
            ViewBag.UnitID = new SelectList(db.Units, "Id", "Name", staff.UnitID);
            return View(staff);
        }

        // GET: Admin/Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name", staff.DepartmentID);
            ViewBag.MainUnitID = new SelectList(db.MainUnits, "Id", "Name", staff.MainUnitID);
            ViewBag.SubUnitID = new SelectList(db.SubUnits, "Id", "Name", staff.SubUnitID);
            ViewBag.UnitID = new SelectList(db.Units, "Id", "Name", staff.UnitID);
            return View(staff);
        }

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,IdentityNo,Designation,MobileNumber,TelephoneNumber,DepartmentID,SubUnitID,UnitID,MainUnitID,Description,Note,IsActive")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "Id", "Name", staff.DepartmentID);
            ViewBag.MainUnitID = new SelectList(db.MainUnits, "Id", "Name", staff.MainUnitID);
            ViewBag.SubUnitID = new SelectList(db.SubUnits, "Id", "Name", staff.SubUnitID);
            ViewBag.UnitID = new SelectList(db.Units, "Id", "Name", staff.UnitID);
            return View(staff);
        }

        // GET: Admin/Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Admin/Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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

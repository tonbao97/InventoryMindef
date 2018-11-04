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
using Inventory.CustomFilter;
using Service;

namespace Inventory.Areas.Admin.Controllers
{
    [Authorize]
    public class StaffsController : Controller
    {
        private IStaffService staffService;
        private IMainUnitService mainUnitService;
        private IUnitService unitService;
        private ISubUnitService subUnitService;
        private IDepartmentService departmentService;

        public StaffsController(IStaffService staffService, IMainUnitService mainUnitService, 
            IUnitService unitService, ISubUnitService subUnitService, IDepartmentService departmentService)
        {
            this.staffService = staffService;
            this.mainUnitService = mainUnitService;
            this.unitService = unitService;
            this.subUnitService = subUnitService;
            this.departmentService = departmentService;
        }




        // GET: Admin/Staffs
        [CustomFilters]
        public ActionResult Index()
        {
            var staffs = staffService.GetStaffs().AsQueryable().Include(s => s.Department).Include(s => s.MainUnit).Include(s => s.SubUnit).Include(s => s.Unit);
            return View(staffs.ToList());
        }

        // GET: Admin/Staffs/Details/5
        [CustomFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = staffService.GetStaffById(id.Value);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Admin/Staffs/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(departmentService.GetDepartments(), "Id", "Name");
            ViewBag.MainUnitID = new SelectList(mainUnitService.GetMainUnits(), "Id", "Name");
            ViewBag.SubUnitID = new SelectList(subUnitService.GetSubUnits(), "Id", "Name");
            ViewBag.UnitID = new SelectList(unitService.GetUnits(), "Id", "Name");
            return View();
        }

        // POST: Admin/Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters]
        public ActionResult Create([Bind(Include = "Id,Name,IdentityNo,Designation,MobileNumber,TelephoneNumber,DepartmentID,SubUnitID,UnitID,MainUnitID,Description,Note,IsActive")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                staffService.CreateStaff(staff);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(departmentService.GetDepartments(), "Id", "Name", staff.DepartmentID);
            ViewBag.MainUnitID = new SelectList(mainUnitService.GetMainUnits(), "Id", "Name", staff.MainUnitID);
            ViewBag.SubUnitID = new SelectList(subUnitService.GetSubUnits(), "Id", "Name", staff.SubUnitID);
            ViewBag.UnitID = new SelectList(unitService.GetUnits(), "Id", "Name", staff.UnitID);
            return View(staff);
        }

        // GET: Admin/Staffs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = staffService.GetStaffById(id.Value);
            if (staff == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(departmentService.GetDepartments(), "Id", "Name", staff.DepartmentID);
            ViewBag.MainUnitID = new SelectList(mainUnitService.GetMainUnits(), "Id", "Name", staff.MainUnitID);
            ViewBag.SubUnitID = new SelectList(subUnitService.GetSubUnits(), "Id", "Name", staff.SubUnitID);
            ViewBag.UnitID = new SelectList(unitService.GetUnits(), "Id", "Name", staff.UnitID);
            return View(staff);
        }

        // POST: Admin/Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters]
        public ActionResult Edit([Bind(Include = "Id,Name,IdentityNo,Designation,MobileNumber,TelephoneNumber,DepartmentID,SubUnitID,UnitID,MainUnitID,Description,Note,IsActive")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                staffService.EditStaff(staff);
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(departmentService.GetDepartments(), "Id", "Name", staff.DepartmentID);
            ViewBag.MainUnitID = new SelectList(mainUnitService.GetMainUnits(), "Id", "Name", staff.MainUnitID);
            ViewBag.SubUnitID = new SelectList(subUnitService.GetSubUnits(), "Id", "Name", staff.SubUnitID);
            ViewBag.UnitID = new SelectList(unitService.GetUnits(), "Id", "Name", staff.UnitID);
            return View(staff);
        }

        // GET: Admin/Staffs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = staffService.GetStaffById(id.Value);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Admin/Staffs/Delete/5
        [CustomFilters]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            staffService.DeleteStaff(id);
            return RedirectToAction("Index");
        }

        
    }
}

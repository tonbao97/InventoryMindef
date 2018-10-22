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
using Service;

namespace Inventory.Areas.Admin.Controllers
{
    public class SubUnitsController : Controller
    {
        private ISubUnitService SubUnitService;

        public SubUnitsController(ISubUnitService SubUnitService)
        {
            this.SubUnitService = SubUnitService;
        }

        // GET: Admin/SubUnits
        public ActionResult Index()
        {
            return View(SubUnitService.GetSubUnits().ToList());
        }

        // GET: Admin/SubUnits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubUnit SubUnit = SubUnitService.GetSubUnitById(id.Value);
            if (SubUnit == null)
            {
                return HttpNotFound();
            }
            return View(SubUnit);
        }

        // GET: Admin/SubUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SubUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Note,IsActive")] SubUnit SubUnit)
        {
            if (ModelState.IsValid)
            {
                SubUnitService.CreateSubUnit(SubUnit);
                return RedirectToAction("Index");
            }

            return View(SubUnit);
        }

        // GET: Admin/SubUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubUnit SubUnit = SubUnitService.GetSubUnitById(id.Value);
            if (SubUnit == null)
            {
                return HttpNotFound();
            }
            return View(SubUnit);
        }

        // POST: Admin/SubUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Note,IsActive")] SubUnit SubUnit)
        {
            if (ModelState.IsValid)
            {
                SubUnitService.EditSubUnit(SubUnit);
                return RedirectToAction("Index");
            }
            return View(SubUnit);
        }

        // GET: Admin/SubUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubUnit SubUnit = SubUnitService.GetSubUnitById(id.Value);
            if (SubUnit == null)
            {
                return HttpNotFound();
            }
            return View(SubUnit);
        }

        // POST: Admin/SubUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubUnitService.DeleteSubUnit(id);
            return RedirectToAction("Index");
        }


    }
}

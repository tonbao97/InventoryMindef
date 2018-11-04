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
    public class UnitsController : Controller
    {
        private IUnitService UnitService;

        public UnitsController(IUnitService UnitService)
        {
            this.UnitService = UnitService;
        }

        // GET: Admin/Units
        [CustomFilters]
        public ActionResult Index()
        {
            return View(UnitService.GetUnits().ToList());
        }

        // GET: Admin/Units/Details/5
        [CustomFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit Unit = UnitService.GetUnitById(id.Value);
            if (Unit == null)
            {
                return HttpNotFound();
            }
            return View(Unit);
        }

        // GET: Admin/Units/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Units/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Note,IsActive")] Unit Unit)
        {
            if (ModelState.IsValid)
            {
                UnitService.CreateUnit(Unit);
                return RedirectToAction("Index");
            }

            return View(Unit);
        }

        // GET: Admin/Units/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit Unit = UnitService.GetUnitById(id.Value);
            if (Unit == null)
            {
                return HttpNotFound();
            }
            return View(Unit);
        }

        // POST: Admin/Units/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Note,IsActive")] Unit Unit)
        {
            if (ModelState.IsValid)
            {
                UnitService.EditUnit(Unit);
                return RedirectToAction("Index");
            }
            return View(Unit);
        }

        // GET: Admin/Units/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unit Unit = UnitService.GetUnitById(id.Value);
            if (Unit == null)
            {
                return HttpNotFound();
            }
            return View(Unit);
        }

        // POST: Admin/Units/Delete/5
        [CustomFilters]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UnitService.DeleteUnit(id);
            return RedirectToAction("Index");
        }


    }
}

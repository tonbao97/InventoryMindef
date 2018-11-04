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
    public class MainUnitsController : Controller
    {
        private IMainUnitService mainUnitService;

        public MainUnitsController(IMainUnitService mainUnitService)
        {
            this.mainUnitService = mainUnitService;
        }

        // GET: Admin/MainUnits
        [CustomFilters]
        public ActionResult Index()
        {
            return View(mainUnitService.GetMainUnits().ToList());
        }

        // GET: Admin/MainUnits/Details/5
        [CustomFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainUnit mainUnit =  mainUnitService.GetMainUnitById(id.Value);
            if (mainUnit == null)
            {
                return HttpNotFound();
            }
            return View(mainUnit);
        }

        // GET: Admin/MainUnits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/MainUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Note,IsActive")] MainUnit mainUnit)
        {
            if (ModelState.IsValid)
            {
                mainUnitService.CreateMainUnit(mainUnit);
                return RedirectToAction("Index");
            }

            return View(mainUnit);
        }

        // GET: Admin/MainUnits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainUnit mainUnit = mainUnitService.GetMainUnitById(id.Value);
            if (mainUnit == null)
            {
                return HttpNotFound();
            }
            return View(mainUnit);
        }

        // POST: Admin/MainUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomFilters]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Note,IsActive")] MainUnit mainUnit)
        {
            if (ModelState.IsValid)
            {
                mainUnitService.EditMainUnit(mainUnit);
                return RedirectToAction("Index");
            }
            return View(mainUnit);
        }

        // GET: Admin/MainUnits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MainUnit mainUnit = mainUnitService.GetMainUnitById(id.Value);
            if (mainUnit == null)
            {
                return HttpNotFound();
            }
            return View(mainUnit);
        }

        // POST: Admin/MainUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomFilters]
        public ActionResult DeleteConfirmed(int id)
        {
            mainUnitService.DeleteMainUnit(id);
            return RedirectToAction("Index");
        }

        
    }
}

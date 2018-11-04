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

    public class EquipmentTypesController : BaseController
    {
        private IEquipmentTypeService equipmentTypeService;

        public EquipmentTypesController(IEquipmentTypeService equipmentTypeService)
        {
            this.equipmentTypeService = equipmentTypeService;
        }



        // GET: Admin/EquipmentTypes
        [CustomFilters]
        public ActionResult Index()
        {
            return View(equipmentTypeService.GetEquipmentTypes().ToList());
        }

        // GET: Admin/EquipmentTypes/Details/5
        [CustomFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentType equipmentType = equipmentTypeService.GetEquipmentTypeById(id.Value);
            if (equipmentType == null)
            {
                return HttpNotFound();
            }
            return View(equipmentType);
        }

        // GET: Admin/EquipmentTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/EquipmentTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Note,IsActive")] EquipmentType equipmentType)
        {
            if (ModelState.IsValid)
            {
                equipmentTypeService.CreateEquipmentType(equipmentType);
                return RedirectToAction("Index");
            }

            return View(equipmentType);
        }

        // GET: Admin/EquipmentTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentType equipmentType = equipmentTypeService.GetEquipmentTypeById(id.Value);
            if (equipmentType == null)
            {
                return HttpNotFound();
            }
            return View(equipmentType);
        }

        // POST: Admin/EquipmentTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Note,IsActive")] EquipmentType equipmentType)
        {
            if (ModelState.IsValid)
            {
                equipmentTypeService.EditEquipmentType(equipmentType);
                return RedirectToAction("Index");
            }
            return View(equipmentType);
        }

        // GET: Admin/EquipmentTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EquipmentType equipmentType = equipmentTypeService.GetEquipmentTypeById(id.Value);
            if (equipmentType == null)
            {
                return HttpNotFound();
            }
            return View(equipmentType);
        }

        // POST: Admin/EquipmentTypes/Delete/5
        [CustomFilters]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipmentTypeService.DeleteEquipmentType(id);
            return RedirectToAction("Index");
        }

    }
}

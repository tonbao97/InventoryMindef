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
    public class EquipmentsController : BaseController
    {
        private ICategoryService categoryService;
        private IEquipmentService equipmentService;
        private IBrandService brandService;
        private ISupplierService supplierService;

        public EquipmentsController(IEquipmentService equipmentService, ICategoryService categoryService, ISupplierService supplierService,
            IBrandService brandService)
        {
            this.equipmentService = equipmentService;
            this.categoryService = categoryService;
            this.brandService = brandService;
            this.supplierService = supplierService;
        }

        // GET: Admin/Equipments
        public ActionResult Index()
        {
            var equipments = equipmentService.GetEquipments();
            return View(equipments.ToList());
        }

        // GET: Admin/Equipments/Details/5
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
        public ActionResult Create()
        {
            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name");
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys(), "Id", "Name");
            ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name");
            return View();
        }

        // POST: Admin/Equipments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryID,Quantity,BrandID,SupplierID,Price,Name,Description,Note,IsActive")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                equipmentService.CreateEquipment(equipment);
                return RedirectToAction("Index");
            }

            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name", equipment.BrandID);
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys(), "Id", "Name", equipment.CategoryID);
            return View(equipment);
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
            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name", equipment.BrandID);
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys(), "Id", "Name", equipment.CategoryID);
            return View(equipment);
        }

        // POST: Admin/Equipments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryID,Quantity,BrandID,SupplierID,Price,Name,Description,Note,IsActive")] Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                equipmentService.EditEquipment(equipment);
                return RedirectToAction("Index");
            }
            ViewBag.BrandID = new SelectList(brandService.GetBrands(), "Id", "Name", equipment.BrandID);
            ViewBag.CategoryID = new SelectList(categoryService.GetCategorys(), "Id", "Name", equipment.CategoryID);
            return View(equipment);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            equipmentService.DeleteEquipment(id);
            return RedirectToAction("Index");
        }

        
    }
}

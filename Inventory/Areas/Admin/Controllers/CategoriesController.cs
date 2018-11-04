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
    public class CategoriesController : BaseController
    {
        private ICategoryService categoryService;
        private IEquipmentTypeService equipmentTypeService;

        public CategoriesController(IEquipmentTypeService equipmentTypeService, ICategoryService categoryService)
        {
            this.equipmentTypeService = equipmentTypeService;
            this.categoryService = categoryService;
        }

        // GET: Admin/Categories
        [CustomFilters]
        public ActionResult Index()
        {
            var categories = categoryService.GetCategorys();
            return View(categories.ToList());
        }

        // GET: Admin/Categories/Details/5
        [CustomFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryService.GetCategoryById(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Categories/Create
        public ActionResult Create()
        {
            ViewBag.EquipmentTypeID = new SelectList(equipmentTypeService.GetEquipmentTypes(), "Id", "Name");
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EquipmentTypeID,Name,Description,Note,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.CreateCategory(category);
                return RedirectToAction("Index");
            }

            ViewBag.EquipmentTypeID = new SelectList(equipmentTypeService.GetEquipmentTypes(), "Id", "Name", category.EquipmentTypeID);
            return View(category);
        }

        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryService.GetCategoryById(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquipmentTypeID = new SelectList(equipmentTypeService.GetEquipmentTypes(), "Id", "Name", category.EquipmentTypeID);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EquipmentTypeID,Name,Description,Note,IsActive")] Category category)
        {
            if (ModelState.IsValid)
            {
                categoryService.EditCategory(category);
                return RedirectToAction("Index");
            }
            ViewBag.EquipmentTypeID = new SelectList(equipmentTypeService.GetEquipmentTypes(), "Id", "Name", category.EquipmentTypeID);
            return View(category);
        }

        // GET: Admin/Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryService.GetCategoryById(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Categories/Delete/5
        [CustomFilters]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            categoryService.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        
    }
}

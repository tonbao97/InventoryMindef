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
    public class BrandsController : Controller
    {
        private IBrandService BrandService;

        public BrandsController(IBrandService BrandService)
        {
            this.BrandService = BrandService;
        }

        // GET: Admin/Brands
        public ActionResult Index()
        {
            return View(BrandService.GetBrands().ToList());
        }

        // GET: Admin/Brands/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand Brand = BrandService.GetBrandById(id.Value);
            if (Brand == null)
            {
                return HttpNotFound();
            }
            return View(Brand);
        }

        // GET: Admin/Brands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Note,IsActive")] Brand Brand)
        {
            if (ModelState.IsValid)
            {
                BrandService.CreateBrand(Brand);
                return RedirectToAction("Index");
            }

            return View(Brand);
        }

        // GET: Admin/Brands/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand Brand = BrandService.GetBrandById(id.Value);
            if (Brand == null)
            {
                return HttpNotFound();
            }
            return View(Brand);
        }

        // POST: Admin/Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Note,IsActive")] Brand Brand)
        {
            if (ModelState.IsValid)
            {
                BrandService.EditBrand(Brand);
                return RedirectToAction("Index");
            }
            return View(Brand);
        }

        // GET: Admin/Brands/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brand Brand = BrandService.GetBrandById(id.Value);
            if (Brand == null)
            {
                return HttpNotFound();
            }
            return View(Brand);
        }

        // POST: Admin/Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BrandService.DeleteBrand(id);
            return RedirectToAction("Index");
        }


    }
}

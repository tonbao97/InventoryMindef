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
    public class DeliveryPackagesController : BaseController
    {
        private IDeliveryPackageService deliveryPackageService;
        private ISupplierService supplierService;

        public DeliveryPackagesController(IDeliveryPackageService deliveryPackageService, ISupplierService supplierService)
        {
            this.deliveryPackageService = deliveryPackageService;
            this.supplierService = supplierService;
        }


        // GET: Admin/DeliveryPackages
        public ActionResult Index()
        {
            var deliveryPackages = deliveryPackageService.GetDeliveryPackages().AsQueryable().Include(d => d.Supplier);
            return View(deliveryPackages.ToList());
        }

        // GET: Admin/DeliveryPackages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryPackage deliveryPackage =  deliveryPackageService.GetDeliveryPackageById(id.Value);
            if (deliveryPackage == null)
            {
                return HttpNotFound();
            }
            return View(deliveryPackage);
        }

        // GET: Admin/DeliveryPackages/Create
        public ActionResult Create()
        {
            ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name");
            return View();
        }

        // POST: Admin/DeliveryPackages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupplierID,DeliveryOrderNo,DODate,ReceiverID,IsActive,IsConfirmed,Description,Remarks,Completed")] DeliveryPackage deliveryPackage)
        {
            if (ModelState.IsValid)
            {
                deliveryPackageService.CreateDeliveryPackage(deliveryPackage);
                return RedirectToAction("Index");
            }

            ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name", deliveryPackage.SupplierID);
            return View(deliveryPackage);
        }

        // GET: Admin/DeliveryPackages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryPackage deliveryPackage = deliveryPackageService.GetDeliveryPackageById(id.Value);
            if (deliveryPackage == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name", deliveryPackage.SupplierID);
            return View(deliveryPackage);
        }

        // POST: Admin/DeliveryPackages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupplierID,DeliveryOrderNo,DODate,ReceiverID,IsActive,IsConfirmed,Description,Remarks,Completed")] DeliveryPackage deliveryPackage)
        {
            if (ModelState.IsValid)
            {
                deliveryPackageService.EditDeliveryPackage(deliveryPackage);
                return RedirectToAction("Index");
            }
            ViewBag.SupplierID = new SelectList(supplierService.GetSuppliers(), "Id", "Name", deliveryPackage.SupplierID);
            return View(deliveryPackage);
        }

        // GET: Admin/DeliveryPackages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DeliveryPackage deliveryPackage = deliveryPackageService.GetDeliveryPackageById(id.Value);
            if (deliveryPackage == null)
            {
                return HttpNotFound();
            }
            return View(deliveryPackage);
        }

        // POST: Admin/DeliveryPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            deliveryPackageService.DeleteDeliveryPackage(id);
            return RedirectToAction("Index");
        }
        
    }
}

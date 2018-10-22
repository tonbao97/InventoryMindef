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
    public class DepartmentsController : Controller
    {
        private IDepartmentService DepartmentService;

        public DepartmentsController(IDepartmentService DepartmentService)
        {
            this.DepartmentService = DepartmentService;
        }

        // GET: Admin/Departments
        public ActionResult Index()
        {
            return View(DepartmentService.GetDepartments().ToList());
        }

        // GET: Admin/Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department Department = DepartmentService.GetDepartmentById(id.Value);
            if (Department == null)
            {
                return HttpNotFound();
            }
            return View(Department);
        }

        // GET: Admin/Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Note,IsActive")] Department Department)
        {
            if (ModelState.IsValid)
            {
                DepartmentService.CreateDepartment(Department);
                return RedirectToAction("Index");
            }

            return View(Department);
        }

        // GET: Admin/Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department Department = DepartmentService.GetDepartmentById(id.Value);
            if (Department == null)
            {
                return HttpNotFound();
            }
            return View(Department);
        }

        // POST: Admin/Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Note,IsActive")] Department Department)
        {
            if (ModelState.IsValid)
            {
                DepartmentService.EditDepartment(Department);
                return RedirectToAction("Index");
            }
            return View(Department);
        }

        // GET: Admin/Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department Department = DepartmentService.GetDepartmentById(id.Value);
            if (Department == null)
            {
                return HttpNotFound();
            }
            return View(Department);
        }

        // POST: Admin/Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartmentService.DeleteDepartment(id);
            return RedirectToAction("Index");
        }


    }
}

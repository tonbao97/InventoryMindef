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
    public class StatusController : Controller
    {
        private IStatusService StatusService;

        public StatusController(IStatusService StatusService)
        {
            this.StatusService = StatusService;
        }

        // GET: Admin/Statuss
        [CustomFilters]
        public ActionResult Index()
        {
            return View(StatusService.GetStatuss().ToList());
        }

        // GET: Admin/Statuss/Details/5
        [CustomFilters]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status Status = StatusService.GetStatusById(id.Value);
            if (Status == null)
            {
                return HttpNotFound();
            }
            return View(Status);
        }

        // GET: Admin/Statuss/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Statuss/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Note,IsActive")] Status Status)
        {
            if (ModelState.IsValid)
            {
                StatusService.CreateStatus(Status);
                return RedirectToAction("Index");
            }

            return View(Status);
        }

        // GET: Admin/Statuss/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status Status = StatusService.GetStatusById(id.Value);
            if (Status == null)
            {
                return HttpNotFound();
            }
            return View(Status);
        }

        // POST: Admin/Statuss/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [CustomFilters]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Note,IsActive")] Status Status)
        {
            if (ModelState.IsValid)
            {
                StatusService.EditStatus(Status);
                return RedirectToAction("Index");
            }
            return View(Status);
        }

        // GET: Admin/Statuss/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status Status = StatusService.GetStatusById(id.Value);
            if (Status == null)
            {
                return HttpNotFound();
            }
            return View(Status);
        }

        // POST: Admin/Statuss/Delete/5
        [CustomFilters]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StatusService.DeleteStatus(id);
            return RedirectToAction("Index");
        }


    }
}

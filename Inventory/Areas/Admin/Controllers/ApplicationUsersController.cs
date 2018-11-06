using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data;
using Data.Models;
using Inventory.CustomFilter;
using Microsoft.AspNet.Identity.Owin;

namespace Inventory.Areas.Admin.Controllers
{
    public class UsersController : BaseController
    {
        private InventoryEntities db = new InventoryEntities();

        private ApplicationUserManager _userManager;


        public ApplicationUserManager UserManager
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Users
        [CustomFilters]
        public ActionResult Index()
        {
            var Users = db.Users.Include(a => a.Staff);
            return View(Users.ToList());
        }

        // GET: Admin/Users/Details/5
        [CustomFilters]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }
        [CustomFilters]
        public async Task<ActionResult> Confirm(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsConfirmed = true;
            await UserManager.UpdateAsync(user);
            db.SaveChanges();
            await UserManager.SendEmailAsync(user.Id, "Account confirmation", "Your account in inventory system has been confirmed!");
            return RedirectToAction("Details", new { id = id});
        }
        [CustomFilters]
        public async Task<ActionResult> Active(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsActive = true;
            await UserManager.UpdateAsync(user);
            return RedirectToAction("Details", new { id = id });
        }
        [CustomFilters]
        public async Task<ActionResult> Deactive(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = UserManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return HttpNotFound();
            }
            user.IsActive = false;
            await UserManager.UpdateAsync(user);
            return RedirectToAction("Details", new { id = id });
        }

        // GET: Admin/Users/Create
        //public ActionResult Create()
        //{
        //    ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Name");
        //    return View();
        //}

        // POST: Admin/Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,DateCreated,StaffID,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(applicationUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Name", applicationUser.StaffID);
        //    return View(applicationUser);
        //}

        // GET: Admin/Users/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = db.Users.Find(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Name", applicationUser.StaffID);
        //    return View(applicationUser);
        //}

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,DateCreated,StaffID,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(applicationUser).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.StaffID = new SelectList(db.Staffs, "Id", "Name", applicationUser.StaffID);
        //    return View(applicationUser);
        //}

        // GET: Admin/Users/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = db.Users.Find(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        // POST: Admin/Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    ApplicationUser applicationUser = db.Users.Find(id);
        //    db.Users.Remove(applicationUser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using Data.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inventory.Controllers
{
    public class UserManageController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private IUserRoleStore<ApplicationUser> _userRoleManager;


        public UserManageController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }
        // GET: UserManage
        public ActionResult Index()
        {
            return View();
        }
    }
}
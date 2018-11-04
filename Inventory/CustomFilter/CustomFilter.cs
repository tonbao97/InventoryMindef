using Data;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Inventory.CustomFilter
{
    public class CustomFilters : ActionFilterAttribute
    {
        private InventoryEntities db = new InventoryEntities();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ip = filterContext.HttpContext.Request.UserHostAddress;
            var user = filterContext.HttpContext.User.Identity.Name;
            var datetime = filterContext.HttpContext.Timestamp;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;
            var param = filterContext.ActionParameters.Values.FirstOrDefault() == null ? "null" 
                : filterContext.ActionParameters.Values.FirstOrDefault().ToString();
            var url = filterContext.HttpContext.Request.Url.ToString();
            var rawurl = filterContext.HttpContext.Request.RawUrl.ToString();

            var log = new Logging()
            {
                IP = ip,
                UserEmail = user,
                Action = action,
                Controller = controller,
                Params = param,
                Time = datetime,
                Url = url,
                RawUrl = rawurl
            };
            db.Loggings.Add(log);
            db.SaveChanges();
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
    }
}
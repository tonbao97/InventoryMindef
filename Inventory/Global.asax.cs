using Data;
using Inventory.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Inventory
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Data.Entity.Database.SetInitializer<InventoryEntities>(null);
            //System.Data.Entity.Database.SetInitializer(new InventorySampleData());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
    //        GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
    //.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    //        GlobalConfiguration.Configuration.Formatters
    //            .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Run();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;

namespace Inventory
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var enableCorsAttribute = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "GetBrands",
                "api/getbrands",
                new { controller = "Equipments", action = "GetBrands" }
            );
            config.Routes.MapHttpRoute(
                "GetEquipments",
                "api/getequipments",
                new { controller = "Equipments", action = "GetEquipments" }
            );
            config.Routes.MapHttpRoute(
                "GetSuppliers",
                "api/getsuppliers",
                new { controller = "Equipments", action = "GetSuppliers" }
            );
            config.Routes.MapHttpRoute(
                "GetEquipmentTypes",
                "api/getequipmenttypes",
                new { controller = "Equipments", action = "GetEquipmentTypes" }
            );
            config.Routes.MapHttpRoute(
                "GetCategories",
                "api/getcategories",
                new { controller = "Equipments", action = "GetCategories" }
            );
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.EnableCors(enableCorsAttribute);
        }
    }
}

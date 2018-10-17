using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Core.Authentication;
using Data;
using Data.Infrastructure;
using Data.Models;
using Data.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Inventory.App_Start
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<InventoryEntities>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DatabaseFactory>()
                   .As<IDatabaseFactory>()
                   .InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(EquipmentTypeRepository).Assembly)
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(EquipmentTypeService).Assembly)
           .Where(t => t.Name.EndsWith("Service"))
           .AsImplementedInterfaces().InstancePerHttpRequest();
            builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
       .Where(t => t.Name.EndsWith("Authentication"))
       .AsImplementedInterfaces().InstancePerHttpRequest();
            builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InventoryEntities())))
                .As<UserManager<ApplicationUser>>().InstancePerHttpRequest();
            builder.RegisterFilterProvider();
            //builder.RegisterGeneric(typeof(RepositoryBase<>))
            //       .As(typeof(IRepositoryBase<>))
            //       .InstancePerRequest();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}
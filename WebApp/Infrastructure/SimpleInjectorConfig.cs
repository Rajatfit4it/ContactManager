using DAL;
using IDAL;
using Service;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Infrastructure
{
    public class SimpleInjectorConfig
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            container.Register<IContactService, ContactService>(Lifestyle.Scoped);
            container.Register<IRepository<Contact>, Repository<Contact>>(Lifestyle.Scoped);
            container.Register<ContactDB, ContactDB>(Lifestyle.Scoped);
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
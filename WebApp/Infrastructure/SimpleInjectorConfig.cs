using DAL.Repositories;
using DAL.IRepositories;
using DAL;
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
using DAL.DBModel;

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
            container.Register<IDbContext, ContactDB>(Lifestyle.Scoped);
            container.Register<IContactRepository, ContactRepository>(Lifestyle.Scoped);
            container.Register<IEventAggregator, EventAggregator>(Lifestyle.Singleton);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
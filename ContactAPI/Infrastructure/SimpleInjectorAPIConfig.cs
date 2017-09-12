using DAL;
using DAL.DBModel;
using DAL.IRepositories;
using DAL.Repositories;
using Service;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ContactAPI.Infrastructure
{
    public class SimpleInjectorAPIConfig
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            // Register your types, for instance:
            container.Register<IContactService, ContactService>(Lifestyle.Scoped);
            container.Register<IRepository<Contact>, Repository<Contact>>(Lifestyle.Scoped);
            container.Register<IDbContext, ContactDB>(Lifestyle.Scoped);
            container.Register<IContactRepository, ContactRepository>(Lifestyle.Scoped);
            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
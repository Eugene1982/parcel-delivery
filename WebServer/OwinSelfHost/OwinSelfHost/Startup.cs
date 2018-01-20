using System;
using System.Linq;
using System.Reflection;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Logging;
using OwinSelfHost.Domain;
using OwinSelfHost.Repository;
using OwinSelfHost.WebApi;
using Raven.Client.Documents.Session;

namespace OwinSelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            AddDefaultData();
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
            );

            var container = ConfigureDIContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
        }

        private IContainer ConfigureDIContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<Repository.Repository>().As<IRepository>();
            return builder.Build();
        }

        private void AddDefaultData()
        {
            using (IDocumentSession session = DocumentStoreHolder.Store.OpenSession())
            {
                session.ClearDocuments<Department>();
                session.Store(new Department
                {
                    Name = "Mail",
                    WeightMin = 0,
                    WeightMax = 1,
                    CreatedAt = DateTime.Now
                });
                session.Store(new Department
                {
                    Name = "Regular",
                    WeightMin = 1,
                    WeightMax = 10,
                    CreatedAt = DateTime.Now
                });
                session.Store(new Department
                {
                    Name = "Heavy",
                    WeightMin = 10,
                    WeightMax = int.MaxValue,
                    CreatedAt = DateTime.Now
                });
                session.Store(new Department
                {
                    Name = "Insurance",
                    PriceStart = 1000,
                    CreatedAt = DateTime.Now
                });

                session.SaveChanges();
            }
        }

       
    }
}

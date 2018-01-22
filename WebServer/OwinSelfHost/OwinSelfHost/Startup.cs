using System;
using System.Linq;
using System.Reflection;
using Owin;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Logging;
using OwinSelfHost.Domain;
using OwinSelfHost.Helpers;
using OwinSelfHost.Repository;
using OwinSelfHost.Storage;
using OwinSelfHost.WebApi;
using Raven.Client.Documents.Session;

namespace OwinSelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
           
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
            AddDefaultData(container);

        }

        private IContainer ConfigureDIContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<Repository.Repository>().As<IRepository>();
            builder.RegisterType<DistributeParcels>().As<IDistributeParcels>();
            builder.RegisterType<Parser>().As<IParser>();
            builder.Register(c => DocumentStoreHolder.Store.OpenSession()).As<IDocumentSession>().InstancePerDependency();
            return builder.Build();
        }

        private void AddDefaultData(IContainer container) => new DefaultData(container.Resolve<IRepository>()).Upload();
    }
}

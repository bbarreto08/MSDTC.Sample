using Autofac;
using Autofac.Integration.WebApi;
using MSDTC.Sample.Api.App_Start;
using MSDTC.Sample.Api.DbContext.Repository;
using MSDTC.Sample.Api.Interfaces.Repositories;
using MSDTC.Sample.Api.Interfaces.Services;
using MSDTC.Sample.Api.Services;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MSDTC.Sample.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            string databaseSampleA = ConfigurationManager.ConnectionStrings["connectionSampleA"].ConnectionString;
            string databaseSampleB = ConfigurationManager.ConnectionStrings["connectionSampleB"].ConnectionString;

            builder.RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .WithParameter("connectionString", databaseSampleA)
                .InstancePerRequest();

            builder.RegisterType<AuditClientRepository>()
                .As<IAuditClientRepository>()
                .WithParameter("connectionString", databaseSampleB)
                .InstancePerRequest();

            builder.RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerRequest();

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}

using Autofac;
using Autofac.Integration.WebApi;
using MSDTC.Sample.Api.App_Start;
using MSDTC.Sample.Api.DbContext.Repository;
using MSDTC.Sample.Api.Interfaces.Repositories;
using MSDTC.Sample.Api.Interfaces.Services;
using MSDTC.Sample.Api.Interfaces.Utils;
using MSDTC.Sample.Api.Services;
using MSDTC.Sample.Api.Utils;
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
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);                       

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);            

            string databaseSampleA = ConfigurationManager.ConnectionStrings["connectionSampleA"].ConnectionString;
            string databaseSampleB = ConfigurationManager.ConnectionStrings["connectionSampleB"].ConnectionString;

            builder.RegisterType<FeatureToggleManager>()
                .As<IFeatureToggleManager>()
                .SingleInstance();

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

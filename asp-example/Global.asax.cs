using asp_example.Repositories;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace asp_example
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            MigrateToLatestVersion();

            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<TodoesRepository>().As<ITodoesRepository>().InstancePerHttpRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private void MigrateToLatestVersion()
        {
            try
            {
                string connectionString = string.Empty;
                connectionString = System.Configuration.ConfigurationManager
                                        .ConnectionStrings["DefaultConnection"].ConnectionString;

                var configuration = new asp_example.Migrations.Configuration();
                configuration.TargetDatabase =
                    new System.Data.Entity.Infrastructure.DbConnectionInfo(connectionString, "System.Data.SqlClient");

                var migrator = new DbMigrator(configuration);
                migrator.Update();
            }
            catch (Exception e)
            {
                Trace.TraceError("Failed to run migration. {0}", e.StackTrace);
            }
        }
    }
}
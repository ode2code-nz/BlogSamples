using Autofac;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ApiSample.Infrastructure.Data;
using ApiSample.Infrastructure.Interfaces;
using Specify.Autofac;
using Specify.Configuration;
using Specify.Mocks;
using Specs.Library.ApiSample;
using Specs.Library.ApiSample.Builders.ValueSuppliers;
using Specs.Library.ApiSample.Data;
using Specs.Library.ApiSample.Data.SqlServer;
using Specs.Library.ApiSample.Drivers.Api;
using Specs.Library.ApiSample.Extensions;
using Specs.Library.ApiSample.Identity;
using TestStack.BDDfy.Configuration;
using TestStack.Dossier;

namespace Specs.Component.ApiSample
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class ComponentBootstrapper : DefaultAutofacBootstrapper
    {
        private IHost _host;

        //IN MEMORY CONFIG
        public ComponentBootstrapper()
        {
            HtmlReport.ReportHeader = "ToDo";
            HtmlReport.ReportDescription = "Acceptance Specifications";
            HtmlReport.OutputFileName = "ToDo-AcceptanceSpecs.html";
            Configurator.BatchProcessors.HtmlReport.Disable();

            // Because TestStack.Dossier uses NSubstitute need to tell Specify not to mock
            MockFactory = new NullMockFactory();

            AnonymousValueFixture.GlobalValueSuppliers.Add(new CodeValueSupplier());
        }

        protected override Specify.IContainer BuildApplicationContainer()
        {
            _host = TestSettings
                .CreateHost(ConfigureContainerForTests,
                    services => services.SwapScoped<ICurrentUserService, TestCurrentUserService>());
            var container = _host.ToAutofacContainer();

            return new AutofacContainer(container);
        }

        private void ConfigureContainerForTests(ContainerBuilder builder)
        {
            ConfigureContainerForSpecify(builder);

            builder.RegisterType<AsyncApiDriver>().AsSelf();

            // Database
            builder.RegisterType<SqlDb>().As<IDb>().SingleInstance();
            builder.Register(c =>
                {
                    var options = new DbContextOptionsBuilder<AppDbContext>()
                        .UseSqlServer(TestSettings.ConnectionString)
                        .Options;
                    return new DbUpSqlDbFactory(options);
                })
                .As<IDbFactory>()
                .SingleInstance();

            builder.RegisterType<ResetDatabaseAction>().As<IPerScenarioAction>();
            builder.RegisterType<ResetSystemTimeAction>().As<IPerScenarioAction>();
          //  builder.RegisterType<ResetDefaultUser>().As<IPerScenarioAction>();
            builder.RegisterType<DatabaseApplicationAction>().As<IPerApplicationAction>();

            if (TestSettings.InMemoryMode)
            {
                ConfigureForInMemory(builder);
            }
            else
            {
                builder.RegisterType<WebHost>().As<ITestHost>().InstancePerLifetimeScope();
            }

        }

        private void ConfigureForInMemory(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryHost>().As<ITestHost>().InstancePerLifetimeScope();
            builder.Register(ctx =>
                {
                    return _host.GetTestServer();
                })
                .As<TestServer>()
                .SingleInstance();
        }
    }
}
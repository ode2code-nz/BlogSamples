using Autofac;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Interfaces;
using Specify.Autofac;
using Specify.Configuration;
using Specify.Mocks;
using Specs.Library.ToDo;
using Specs.Library.ToDo.Builders.ValueSuppliers;
using Specs.Library.ToDo.Data;
using Specs.Library.ToDo.Data.SqlServer;
using Specs.Library.ToDo.Drivers.Api;
using Specs.Library.ToDo.Extensions;
using Specs.Library.ToDo.Identity;
using TestStack.BDDfy.Configuration;
using TestStack.Dossier;

namespace Specs.Integration.ToDo
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class IntegrationBootstrapper : DefaultAutofacBootstrapper
    {
        private IHost _host;

        //IN MEMORY CONFIG
        public IntegrationBootstrapper()
        {
            HtmlReport.ReportHeader = "ToDo";
            HtmlReport.ReportDescription = "Integration Specifications";
            HtmlReport.OutputFileName = "ToDo-IntegrationSpecs.html";
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
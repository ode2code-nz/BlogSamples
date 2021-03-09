using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using ToDo.Domain.Model.ToDos;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Interfaces;
using Specs.Library.ToDo;
using Specs.Library.ToDo.Data;
using Specs.Library.ToDo.Identity;
using TestStack.Dossier;

namespace Specs.Integration.ToDo.Infrastructure.Persistence
{
    public class DatabasebRetrySucceeds : ScenarioFor<object>
    {
        private AppDbContext _db;
        private ToDoItem _result;

        public void Given_transient_fault_handling_is_enabled()
        {
            TransientFailureCausingCommandInterceptor.Reset();
            var services = new ServiceCollection();

            //Build up the services required for ef to run. We are doing this so that we can use our
            // own SessionContextConnectionInterceptor, which requires di to get access to the httpcontext.
            var efServices = new ServiceCollection();
            efServices.AddEntityFrameworkSqlServer();
            efServices.AddHttpContextAccessor();
            efServices.AddScoped<ICurrentUserService, TestCurrentUserService>();

            var connectionString = TestSettings.ConnectionString;
            services.AddScoped<ICurrentUserService, TestCurrentUserService>();
            services.AddDbContext<AppDbContext>(opt => opt
                .UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure())
                .UseInternalServiceProvider(efServices.BuildServiceProvider())
                .AddInterceptors(new TransientFailureCausingCommandInterceptor()));

            var provider = services.BuildServiceProvider();

            _db = provider.GetRequiredService<AppDbContext>();
            Builder<ToDoItem>.CreateNew().Persist();
        }

        public void When_I_try_to_access_the_database_and_it_is_not_available()
        {
            _result = _db.ToDoItems.FirstOrDefault();
            //var strategy = db.Database.CreateExecutionStrategy();
            //var entity = strategy.Execute(() =>
            //    db.DisposalReasons.FirstOrDefault());
        }

        public void Then_it_should_retry_and_complete_if_database_becomes_available()
        {
            _result.Should().NotBeNull();
            TransientFailureCausingCommandInterceptor.RetryRunningTotal.Should().Be(3);
        }
    }

    public class DatabaseRetryExceedsMaxRetryCount : ScenarioFor<object>
    {
        private AppDbContext _db;
        private RetryLimitExceededException _result;

        public void Given_transient_fault_handling_is_enabled()
        {
            TransientFailureCausingCommandInterceptor.Reset();
            var services = new ServiceCollection();

            //Build up the services required for ef to run. We are doing this so that we can use our
            // own SessionContextConnectionInterceptor, which requires di to get access to the httpcontext.
            var efServices = new ServiceCollection();
            efServices.AddEntityFrameworkSqlServer();
            efServices.AddHttpContextAccessor();
            efServices.AddScoped<ICurrentUserService, TestCurrentUserService>();

            var connectionString = TestSettings.ConnectionString;
            services.AddScoped<ICurrentUserService, TestCurrentUserService>();
            services.AddDbContext<AppDbContext>(opt => opt
                .UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure(maxRetryCount:2))
                .UseInternalServiceProvider(efServices.BuildServiceProvider())
                .AddInterceptors(new TransientFailureCausingCommandInterceptor(5)));

            var provider = services.BuildServiceProvider();


            _db = provider.GetRequiredService<AppDbContext>();
            Builder<ToDoItem>.CreateNew().Persist();
        }

        public void When_I_try_to_access_the_database_and_it_the_retries_exceed_the_max_count()
        {
            try
            {
                var entity = _db.ToDoItems.FirstOrDefault();
            }
            catch (RetryLimitExceededException exception)
            {
                _result = exception;
            }
        }

        public void Then_it_should_throw_a_RetryLimitExceededException()
        {
            _result.Should().NotBeNull();
        }
    }

}
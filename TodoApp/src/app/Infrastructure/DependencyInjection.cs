using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ToDo.Domain.Abstractions;
using ToDo.Infrastructure.Caching;
using ToDo.Infrastructure.Data;
using ToDo.Infrastructure.Data.Repositories;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesForInfrastructureProject(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddLogging()
                .AddEntityFramework(configuration)
                .AddCachedStaticData()
                .AddHealthChecks(configuration);

            return services;
        }

        private static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppDb")));

            services.AddScoped<IUnitOfWork, AppDbContext>();
            services.AddScoped<IQueryDb, QueryDbContext>();

            return services;
        }

        private static IServiceCollection AddCachedStaticData(this IServiceCollection services)
        {
            services.AddLazyCache();
            services.AddSingleton<ICache, Cache>();

            services.AddScoped<IStaticDataRepository, StaticDataRepository>();
            services.Decorate<IStaticDataRepository, CachedStaticDataRepository>();

            return services;
        }

        private static IServiceCollection AddHealthChecks(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddSqlServer(connectionString: configuration["ConnectionStrings:AppDb"],
                    failureStatus: HealthStatus.Unhealthy, tags: new[] { "ready" }); ;

            return services;
        }
    }
}
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Todo.Infrastructure.Ioc;
using Todo.Infrastructure.Logging;
using Serilog;

namespace Todo.Api
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Log.Logger = LoggingHelper.CreateLogger();

            try
            {
                Log.Information("Starting host");

                var host = CreateHostBuilder(args).Build();

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseServiceProviderFactory(new AppServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                        .CaptureStartupErrors(true);
                });
        }
    }
}
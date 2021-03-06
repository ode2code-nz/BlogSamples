using Microsoft.AspNetCore.Builder;
using Serilog;

namespace ApiSample.Infrastructure.Logging
{
    public static class SerilogApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSerilog(this IApplicationBuilder app)
        {
            return app.UseSerilogRequestLogging(opts
                => opts.EnrichDiagnosticContext = LoggingHelper.EnrichFromRequest);
        }
    }
}

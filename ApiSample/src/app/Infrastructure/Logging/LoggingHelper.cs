using System;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace ApiSample.Infrastructure.Logging
{
    public static class LoggingHelper
    {
        public static ILogger CreateLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                // Filter out ASP.NET Core infrastructure logs that are Information and below
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning) // see warnings and errors from the framework, but not informational log events
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", "ToDo.Api")
                .Enrich.WithMachineName()
               // .Enrich.WithEnvironmentName() why isn't this working. Should come
                .Enrich.WithExceptionDetails()
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                .WriteTo.Seq(Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341")
                .CreateLogger();
        }

        /// <summary>
        /// additional metadata to Serilog's summary request log, such as the Request's hostname,
        /// the Response's content-type, or the selected Endpoint Name from the endpoint routing
        /// middleware used in ASP.NET Core 3.0.
        /// See https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-logging-the-selected-endpoint-name-with-serilog/
        /// </summary>
        public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;

            // Set all the common properties available for every request
            diagnosticContext.Set("Host", request.Host);
            diagnosticContext.Set("Protocol", request.Protocol);
            diagnosticContext.Set("Scheme", request.Scheme);

            // Only set it if available. 
            if (request.QueryString.HasValue)
            {
                diagnosticContext.Set("QueryString", request.QueryString.Value);
            }

            // Set the content-type of the Response at this point
            diagnosticContext.Set("ContentType", httpContext.Response.ContentType);

            // Retrieve the IEndpointFeature selected for the request
            var endpoint = httpContext.GetEndpoint();
            if (endpoint is object) // endpoint != null
            {
                diagnosticContext.Set("EndpointName", endpoint.DisplayName);
            }
        }
    }
}

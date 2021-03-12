using System;
using Microsoft.Extensions.Logging;

namespace ApiSample.Infrastructure.Logging
{
    public static class LogMessages
    {
        private static readonly Action<ILogger, string, long, Exception> _handlerPerformance;
        static LogMessages()
        {
            _handlerPerformance = LoggerMessage.Define<string, long>(LogLevel.Information, 0,
                "{Handler} code took {ElapsedMilliseconds}.");
        }

        public static void LogHandlerPerformance(this ILogger logger, string handlerName,
            long elapsedMilliseconds)
        {
            _handlerPerformance(logger, handlerName, elapsedMilliseconds, null);
        }
    }
}
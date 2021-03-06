using System;

namespace Specs.Library.ApiSample.Drivers.Api
{
    public class ApiDriverException : Exception
    {
        public ApiDriverException(string message)
            : base(message) { }

        public ApiDriverException(string message, Exception inner)
            : base(message, inner) { }
    }
}
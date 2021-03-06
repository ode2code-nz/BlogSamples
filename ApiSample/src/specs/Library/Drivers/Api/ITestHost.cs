using System;
using System.Net.Http;

namespace Specs.Library.ApiSample.Drivers.Api
{
    public interface ITestHost
    {
        Uri BaseAddress { get; }
        string AppName { get; }
        HttpClient Client { get; }
        void Start();
        void Stop();
    }
}
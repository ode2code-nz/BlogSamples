using System;
using Todo.Domain.Common;

namespace Specs.Library.Todo.Helpers
{
    public class TestableSystemTime : IDisposable
    {
        public TestableSystemTime(DateTime dateTime)
        {
            SystemTime.UtcNow = () => dateTime;
        }

        public TestableSystemTime(Func<DateTime> dateTimeFactory)
        {
            SystemTime.UtcNow = dateTimeFactory;
        }

        public void Dispose()
        {
            SystemTime.Reset();
        }
    }
}
using System;

namespace ToDo.Domain.Common
{
    public static class SystemTime
    {
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;

        public static void Reset()
        {
            UtcNow = () => DateTime.UtcNow;
        }
    }
}
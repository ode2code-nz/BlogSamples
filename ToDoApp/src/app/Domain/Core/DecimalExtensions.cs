using System;

namespace ToDo.Domain.Core
{
    public static class DecimalExtensions
    {
        public static decimal Floor(this decimal d, int precision)
        {
            return Math.Floor(d * (decimal)Math.Pow(10, precision)) / (decimal)Math.Pow(10, precision);
        }
    }
}

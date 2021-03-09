using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo.Domain.Core
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> data, Action<T> action)
        {
            foreach (T item in data)
            {
                action(item);
            }
        }

        public static bool In<T>(this T item, params T[] collection)
        {
            return collection.Contains(item);
        }
    }
}
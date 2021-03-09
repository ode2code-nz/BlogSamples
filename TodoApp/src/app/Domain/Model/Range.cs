using System;
using System.Collections.Generic;
using ToDo.Domain.Common;

namespace ToDo.Domain.Model
{
    public class Range<T> : ValueObject where T : IComparable<T>
    {
        protected Range()
        {
        }

        public Range(T min, T max)
        {
            Min = min;
            Max = max;
        }

        public Range(T max)
        {
            Min = default(T);
            Max = max;
        }

        public T Min { get; set; }

        public T Max { get; set; }

        public bool IsValid()
        {
            return Min.CompareTo(Max) <= 0;
        }

        public bool IsInRange(T value)
        {
            return (Min.CompareTo(value) <= 0) && (value.CompareTo(Max) <= 0);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Min;
            yield return Max;
        }
    }
}
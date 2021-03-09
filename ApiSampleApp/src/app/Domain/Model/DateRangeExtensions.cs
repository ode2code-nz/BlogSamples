using System;
using System.Collections.Generic;
using System.Linq;

namespace ToDo.Domain.Model
{
    public static class DateRangeExtensions
    {
        public static List<DateTime> GetDatesMissingFrom(this List<DateRange> dateRanges, DateRange other)
        {
            var sequence = GetDaysFromDateRangeCollection(dateRanges);
            var gaps = other.Days.Except(sequence).ToList();

            return gaps;
        }

        public static bool HaveDatesMissingFrom(this List<DateRange> dateRanges, DateRange other)
        {
            var gaps = dateRanges.GetDatesMissingFrom(other);

            return gaps.Count > 0;
        }

        public static bool HaveOverlappingDates(this List<DateRange> dateRanges)
        {
            foreach (var dateRange in dateRanges)
            {
                var other = dateRanges.Except(new List<DateRange>() {dateRange});
                if (other.Any(x => dateRange.DateInDateRange(x.StartDate) || dateRange.DateInDateRange(x.StartDate)))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool HaveDatesOutOfRangeOf(this List<DateRange> dateRanges, DateRange other)
        {
            return dateRanges.Any(dateRange => !other.DateInDateRange(dateRange.StartDate) || !other.DateInDateRange(dateRange.EndDate));
        }

        private static List<DateTime> GetDaysFromDateRangeCollection(List<DateRange> dateRanges)
        {
            var sequence = new List<DateTime>();
            foreach (var dateRange in dateRanges)
            {
                sequence = sequence.Concat(dateRange.Days).ToList();
            }

            return sequence;
        }
    }
}
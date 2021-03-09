using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Domain.Common;
using ToDo.Domain.Core;

namespace ToDo.Domain.Model
{
    public class DateRange : Range<DateTime>
    {
        public DateTime StartDate
        {
            get => Min;
            private set => Min = value;
        }

        public DateTime EndDate
        {
            get => Max;
            private set => Max = value;
        }

        public int TotalHours => (int)(EndDate - StartDate).TotalHours;

        private DateRange()
        { }

        public DateRange(DateRange dateRange)
        {
            StartDate = dateRange.StartDate;
            EndDate = dateRange.EndDate;
        }

        public DateRange(DateTime date)
        {
            StartDate = date;
            EndDate = date;
        }

        public DateRange(DateTime startDate, DateTime endDate)
        {
            Guard.Against(startDate <= endDate, "StartDate must be before or same as EndDate");

            StartDate = startDate;
            EndDate = endDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartDate;
            yield return EndDate;
        }

        public bool DateInDateRange(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }

        public bool DateRangeInDateRange(DateRange dateRange)
        {
            return DateInDateRange(dateRange.StartDate) && DateInDateRange(dateRange.EndDate);
        }

        //public override string ToString()
        //{
        //    return StartDate == EndDate ? StartDate.ToString() : $"{StartDate}-{EndDate}";
        //}

        public override string ToString()
        {
            return StartDate == EndDate ? StartDate.ToString() : $"{StartDate}-{EndDate}";
        }

        private List<DateTime> _days;
        public List<DateTime> Days
        {
            get
            {
                if (_days == null)
                {
                    var days = (int)(EndDate - StartDate).TotalDays + 1;
                    _days = Enumerable.Range(0, days).Select(add => StartDate.AddDays(add)).ToList();
                }

                return _days;
            }
        }

        public DateRange GasDayStartAndEndDateTime(GasTimeZoneInfo gasTimeZoneInfo)
        {
            return new DateRange(StartDate.Date.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName),
                EndDate.Date.GetGasDayEndDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName));
        }

        public DateTime GasDayStartDate(GasTimeZoneInfo gasTimeZoneInfo)
        {
            return StartDate.GetGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
        }

        public DateTime GasDayEndDate(GasTimeZoneInfo gasTimeZoneInfo)
        {
            return EndDate.GetGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
        }

        public DateRange GasDayStartAndEnd(GasTimeZoneInfo gasTimeZoneInfo)
        {
            return new DateRange(GasDayStartDate(gasTimeZoneInfo), GasDayEndDate(gasTimeZoneInfo));
        }
    }
}

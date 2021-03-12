using System;
using System.Collections.Generic;
using System.Linq;
using ApiSample.Domain.Common;
using ApiSample.Domain.Model;

namespace ApiSample.Domain.Core
{
    public static class DateTimeExtensions
    {
        public const string utcTimeZoneName = "UTC";

        public static DateTime Convert(this DateTime date, string fromZone, string toZone)
        {
            var to = TimeZoneInfo.FindSystemTimeZoneById(toZone);
            var from = TimeZoneInfo.FindSystemTimeZoneById(fromZone);
            return new DateTime(TimeZoneInfo.ConvertTime(date, from, to).Ticks, DateTimeKind.Unspecified);
        }
        public static DateTime ConvertToUtc(this DateTime date, string fromZone)
        {
            return date.Convert(fromZone, utcTimeZoneName);
        }

        public static TimeSpan ConvertToUtc(this TimeSpan time, string fromZone)
        {
            var dateTimeLocal = new DateTime(time.Ticks, DateTimeKind.Unspecified);
            return dateTimeLocal.ConvertToUtc(fromZone).TimeOfDay;
        }

        public static DateTime ConvertToLocal(this DateTime date, string toZone)
        {
            return date.Convert(utcTimeZoneName, toZone);
        }

        public static DateTime GetGasDayStartDateTimeUtc(this DateTime gasDay, GasTimeZoneInfo gasTimeZoneInfo)
        {
            return gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
        }

        public static DateTime GetGasDayStartDateTimeUtc(this DateTime gasDay, int gasDayOffset, string timeZoneName)
        {
            var gasDateTime = new DateTime(gasDay.Year, gasDay.Month, gasDay.Day, gasDayOffset, 0, 0, DateTimeKind.Unspecified);
            return gasDateTime.ConvertToUtc(timeZoneName);
        }

        public static DateTime GetGasDayEndDateTimeUtc(this DateTime gasDay, GasTimeZoneInfo gasTimeZoneInfo)
        {
            return gasDay.GetGasDayEndDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
        }

        public static DateTime GetGasDayEndDateTimeUtc(this DateTime gasDay, int gasDayOffset, string timeZoneName)
        {
            return GetGasDayStartDateTimeUtc(gasDay, gasDayOffset, timeZoneName).AddDays(1).AddHours(-1);
        }

        public static bool IsGasDayDayAhead(this DateTime gasDayDate, GasTimeZoneInfo gasTimeZoneInfo)
        {
            var utcNowGasDay = SystemTime.UtcNow().GetGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
            return gasDayDate == utcNowGasDay.AddDays(1);
        }

        public static bool IsWithinGasDay(this DateTime dateTimeUtc, GasTimeZoneInfo gasTimeZoneInfo)
        {
            return dateTimeUtc.GetGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName) ==
                SystemTime.UtcNow().GetGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
        }

        public static DateTime GetGasDay(this DateTime dateTimeUtc, int gasDayOffset, string timeZoneName)
        {
            var dateTimeLocal = dateTimeUtc.ConvertToLocal(timeZoneName);
            var gasDayDate = new DateTime(dateTimeLocal.Year, dateTimeLocal.Month, dateTimeLocal.Day);
            if (dateTimeLocal.Hour < gasDayOffset)
            {
                gasDayDate = gasDayDate.AddDays(-1);
            }

            return gasDayDate;
        }

        public static List<DateTime> GenerateGasDayHoursUtc(this DateTime gasDay, GasTimeZoneInfo gasTimeZoneInfo)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(gasTimeZoneInfo.TimeZoneName);

            var gasDayHours = new List<DateTime>();
            var startDateTime = new DateTime(gasDay.Year, gasDay.Month, gasDay.Day, gasTimeZoneInfo.GasDayOffset, 0, 0, DateTimeKind.Unspecified);
            var hourlyUtc = startDateTime.ConvertToUtc(gasTimeZoneInfo.TimeZoneName);

            var totalHours = 24;
            for (var i = 0; i < totalHours; i++)
            {
                var newHourly = hourlyUtc.AddHours(i);
                gasDayHours.Add(newHourly);
                if (timeZone.IsInvalidTime(newHourly))
                {
                    totalHours--;
                }
                if (timeZone.IsAmbiguousTime(newHourly))
                {
                    totalHours++;
                }
            }
            return gasDayHours;
        }

        public static IEnumerable<DateTime> GenerateDailyDateRange(this DateTime startDate, DateTime endDate)
        {
            var ts = endDate - startDate;
            for (var i = 0; i < ts.TotalDays; i++)
            {
                yield return startDate.AddDays(i);
            }
        }

        public static IEnumerable<DateTime> GenerateDateRangeGasDayHoursUtc(this DateTime startDate, DateTime endDate, GasTimeZoneInfo gasTimeZoneInfo)
        {
            return GenerateDailyDateRange(startDate, endDate).SelectMany(day => day.GenerateGasDayHoursUtc(gasTimeZoneInfo));
        }

        public static int GetAvailableHoursInGasDay(this DateTime gasDay, int gasDayOffset, string timeZoneName, double withinDayNominationLeadTime = 0)
        {
            var gasDayStartDateTimeUtc = gasDay.GetWithinDayCutoffTimeUtc(withinDayNominationLeadTime, gasDayOffset, timeZoneName);
            var gasDayEndDateTimeUtc = gasDay.GetGasDayEndDateTimeUtc(gasDayOffset, timeZoneName);

            var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);

            return Enumerable.Range(0, 24).TakeWhile(w => gasDayStartDateTimeUtc.AddHours(w) <= gasDayEndDateTimeUtc).Sum(r => tz.IsInvalidTime(gasDayStartDateTimeUtc.AddHours(r)) ? 0 : tz.IsAmbiguousTime(gasDayStartDateTimeUtc.AddHours(r)) ? 2 : 1);
        }


        public static DateTime GetWithinDayCutoffTimeUtc(this DateTime gasDay, double withinDayNominationLeadTime, int gasDayOffset, string timeZoneName)
        {
            var nowUtc = SystemTime.UtcNow();
            var currentGasDay = nowUtc.GetGasDay(gasDayOffset, timeZoneName);
            var gasDayEndDateTimeUtc = gasDay.GetGasDayEndDateTimeUtc(gasDayOffset, timeZoneName);
            var gasDayStartDateTimeUtc = gasDay.GetGasDayStartDateTimeUtc(gasDayOffset, timeZoneName);

            if (gasDay < currentGasDay)
            {
                return gasDayEndDateTimeUtc;
            }

            if (gasDay == currentGasDay)
            {

                var leadTimeHours = (int)(nowUtc.Minute > 30 ? Math.Floor(withinDayNominationLeadTime) : Math.Ceiling(withinDayNominationLeadTime));
                var withinDayCutoffUtc = nowUtc.AddHours(leadTimeHours);
                return withinDayCutoffUtc > gasDayEndDateTimeUtc ? gasDayEndDateTimeUtc : withinDayCutoffUtc;
            }

            return gasDayStartDateTimeUtc;
        }

        public static DateTime GetUtcDateFromGasDayAndHour(this DateTime gasDayAndHour, int gasDayOffset, string timeZoneName)
        {
            var gasDay = gasDayAndHour.Date;
            var gasDayAndHourUtc = ConvertToUtc(gasDayAndHour, timeZoneName);

            var gasDayStartUtc = gasDay.GetGasDayStartDateTimeUtc(gasDayOffset, timeZoneName);

            return gasDayAndHourUtc < gasDayStartUtc ? gasDayAndHourUtc.AddDays(1) : gasDayAndHour.ConvertToUtc(timeZoneName);
        }

        public static DateTime GetGasDayAndHourFromUtcDate(this DateTime utcDate, int gasDayOffset, string timeZoneName)
        {
            var gasDay = utcDate.GetGasDay(gasDayOffset, timeZoneName).ConvertToLocal(timeZoneName);

            return new DateTime(gasDay.Year, gasDay.Month, gasDay.Day, utcDate.Hour, utcDate.Minute, utcDate.Second, DateTimeKind.Utc).ConvertToLocal(timeZoneName);
        }
    }
}


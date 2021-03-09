//using System;
//using System.Collections;
//using System.Collections.Generic;
//using FluentAssertions;
//using NUnit.Framework;
//using Todo.Domain.Common;
//using Todo.Domain.Core;
//using Todo.Domain.Model;

//namespace Specs.Unit.Todo.Domain.Core
//{
//    public class DateTimeExtensionSpecs
//    {
//        public static GasTimeZoneInfo GMT = ValueTypes.GasTimeZoneInfoGmt;

//        [Test]
//        public void DateConvertToUtcThenToLocal_WithDaylightSavingsDateAndTimeZone_DatesAreTheSame()
//        {
//            var timeZoneDate = new DateTime(2019, 6, 1, 5, 0, 0);
//            var toUtcDate = timeZoneDate.ConvertToUtc(GMT.TimeZoneName);
//            var backtoTimezoneDate = toUtcDate.ConvertToLocal(GMT.TimeZoneName);
//            backtoTimezoneDate.Should().Be(timeZoneDate);
//        }

//        [TestCaseSource(typeof(DateTimeExtensionSpecs), nameof(TestCases_TimeZones))]
//        public void TimeSpanConvertToUtc_should_convert_local_timespan_to_Utc(GasTimeZoneInfo fromZone)
//        {
//            var timeZoneTime = new TimeSpan(11, 12, 12);
//            var expected = TimeZoneInfo.ConvertTimeToUtc(new DateTime(timeZoneTime.Ticks),
//                TimeZoneInfo.FindSystemTimeZoneById(fromZone.TimeZoneName));

//            timeZoneTime.ConvertToUtc(fromZone.TimeZoneName).Should().Be(expected.TimeOfDay);
//        }

//        [TestCaseSource(typeof(DateTimeExtensionSpecs), nameof(TestCases_DateTimesInGasDay))]
//        public void GetCurrentGasDay(DateTime gasDateTimeUtc, DateTime expected)
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            gasDateTimeUtc
//                .GetGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName)
//                .Should().Be(expected);
//        }

//        public static IEnumerable<GasTimeZoneInfo> TestCases_TimeZones => new List<GasTimeZoneInfo>() { ValueTypes.GasTimeZoneInfoCet, ValueTypes.GasTimeZoneInfoGmt};

//        public static IEnumerable TestCases_DateTimesInGasDay
//        {
//            get
//            {
//                var gasDayDate = new DateTime(2019, 6, 6);

//                // within gas day
//                yield return new TestCaseData(new DateTime(2019, 6, 6, 4, 0, 0, DateTimeKind.Utc), gasDayDate);
//                yield return new TestCaseData(new DateTime(2019, 6, 6, 6, 0, 0, DateTimeKind.Utc), gasDayDate);
//                yield return new TestCaseData(new DateTime(2019, 6, 6, 10, 10, 5, DateTimeKind.Utc), gasDayDate);
//                yield return new TestCaseData(new DateTime(2019, 6, 6, 10, 30, 0, DateTimeKind.Utc), gasDayDate);
//                yield return new TestCaseData(new DateTime(2019, 6, 6, 23, 59, 59, DateTimeKind.Utc), gasDayDate);
//                yield return new TestCaseData(new DateTime(2019, 6, 7, 0, 0, 0, DateTimeKind.Utc), gasDayDate);
//                yield return new TestCaseData(new DateTime(2019, 6, 7, 0, 0, 1, DateTimeKind.Utc), gasDayDate);
//                yield return new TestCaseData(new DateTime(2019, 6, 7, 3, 59, 59, DateTimeKind.Utc), gasDayDate);

//                // outside gas day
//                yield return new TestCaseData(new DateTime(2019, 6, 6, 3, 59, 59, DateTimeKind.Utc), gasDayDate.AddDays(-1));
//                yield return new TestCaseData(new DateTime(2019, 6, 7, 4, 0, 0, DateTimeKind.Utc), gasDayDate.AddDays(1));
//            }
//        }

//        [Test]
//        public void GetAvailableHoursInGasDay_ForNonDLSTransitionDayCentralEuropeStandardTime1stJan2020_Returns24()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 01, 01);
//            var nowUtc = gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            SystemTime.UtcNow = () => nowUtc;
//            var hours = gasDay.GetAvailableHoursInGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            hours.Should().Be(24);
//        }

//        [Test]
//        public void GetAvailableHoursInGasDay_ForDLSTransitionDayCentralEuropeStandardTime27thOct2019_Returns25()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2019, 10, 26);
//            var nowUtc = gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            SystemTime.UtcNow = () => nowUtc;
//            var hours = gasDay.GetAvailableHoursInGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            hours.Should().Be(25);
//        }

//        [Test]
//        public void GetAvailableHoursInGasDay_ForDLSTransitionDayCentralEuropeStandardTime30thMarch2020_Returns23()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 03, 28);
//            var hours = gasDay.GetAvailableHoursInGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            hours.Should().Be(23);
//        }

//        [Test]
//        public void GetAvailableHoursInGasDay_ForDLSTransitionDayCentralEuropeStandardTime27thOct2019With2HourLead_Returns21()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 03, 28);
//            var nowUtc = gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            SystemTime.UtcNow = () => nowUtc;

//            var hours = gasDay.GetAvailableHoursInGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName, 2);
//            hours.Should().Be(21);
//        }

//        [Test]
//        public void GetAvailableHoursInGasDay_ForDLSTransitionDayCentralEuropeStandardTime27thOct2019With2HourLead_Returns23()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2019, 10, 26);
//            var nowUtc = gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            SystemTime.UtcNow = () => nowUtc;
//            var hours = gasDay.GetAvailableHoursInGasDay(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName, 2);
//            hours.Should().Be(23);
//        }

//        [Test]
//        public void GetWithinDayCutoffTimeUtc_ForPreviousGasDayOntTheSameCalendarDay_ReturnsStartOfGasDay()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 3, 28, 0, 0, 0, DateTimeKind.Utc);
//            var nowUtc = new DateTime(2020, 3, 28, 1, 0, 0, DateTimeKind.Utc);
//            SystemTime.UtcNow = () => nowUtc;
//            var cutoOffTimeUtc = gasDay.GetWithinDayCutoffTimeUtc(2, gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            cutoOffTimeUtc.Should().Be(gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName));
//        }

//        [Test]
//        public void GetWithinDayCutoffTimeUtc_ForWithinDayStartOfDay_ReturnsNowPlus2Hours()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 3, 28, 0, 0, 0, DateTimeKind.Utc);
//            var nowUtc = gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            SystemTime.UtcNow = () => nowUtc;
//            var cutoOffTimeUtc = gasDay.GetWithinDayCutoffTimeUtc(2, gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            cutoOffTimeUtc.Should().Be(nowUtc.AddHours(2));
//        }

//        [Test]
//        public void GetWithinDayCutoffTimeUtc_ForPreviousDay_ReturnsEndOfGasDay()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 3, 27, 0, 0, 0, DateTimeKind.Utc);
//            var nowUtc = new DateTime(2020, 3, 28, 5, 0, 0, DateTimeKind.Utc);
//            SystemTime.UtcNow = () => nowUtc;
//            var cutoOffTimeUtc = gasDay.GetWithinDayCutoffTimeUtc(2, gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            cutoOffTimeUtc.Should().Be(gasDay.GetGasDayEndDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName));
//        }


//        [Test]
//        public void GetWithinDayCutoffTimeUtc_ForDayAhead_ReturnsStartOfGasDay()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 3, 28, 0, 0, 0, DateTimeKind.Utc);
//            var nowUtc = new DateTime(2020, 3, 27, 10, 0, 0, DateTimeKind.Utc);
//            SystemTime.UtcNow = () => nowUtc;
//            var cutoOffTimeUtc = gasDay.GetWithinDayCutoffTimeUtc(2, gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            cutoOffTimeUtc.Should().Be(gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName));
//        }

//        [Test]
//        public void GetWithinDayCutoffTimeUtc_For5DaysAhead_ReturnsStartOfGasDay()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 3, 28, 0, 0, 0, DateTimeKind.Utc);
//            var nowUtc = new DateTime(2020, 3, 23, 10, 0, 0, DateTimeKind.Utc);
//            SystemTime.UtcNow = () => nowUtc;
//            var cutoOffTimeUtc = gasDay.GetWithinDayCutoffTimeUtc(2, gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            cutoOffTimeUtc.Should().Be(gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName));
//        }

//        [Test]
//        public void GetUtcDateFromGasDayAndHour_WithGasDayStartLocal_ReturnsGasDayStartUtc()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDateTime = new DateTime(2020, 1, 1, 6, 0, 0, DateTimeKind.Unspecified);
//            var gasDayStartDateTimeUtc = gasDateTime.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);

//            var gasDateTimeUtc = gasDateTime.GetUtcDateFromGasDayAndHour(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            gasDateTimeUtc.Should().Be(gasDayStartDateTimeUtc);
//        }

//        [Test]
//        public void GetUtcDateFromGasDayAndHour_WithGasDayHour5WithOffset6_ReturnsNextCalendarGasDay4Utc()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDateTime = new DateTime(2020, 1, 1, 5, 0, 0, DateTimeKind.Unspecified);
//            var gasDayEndDateTimeUtc = gasDateTime.GetGasDayEndDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);

//            var gasDateTimeUtc = gasDateTime.GetUtcDateFromGasDayAndHour(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            gasDateTimeUtc.Should().Be(gasDayEndDateTimeUtc);
//        }

//        [Test]
//        public void GetUtcDateFromGasDayAndHour_WithGasDayHour7WithOffset6_ReturnsCalendarGasDay6Utc()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDateTime = new DateTime(2020, 1, 1, 7, 0, 0, DateTimeKind.Unspecified);
//            var gasDayStartDateTimeUtc = gasDateTime.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);

//            var gasDateTimeUtc = gasDateTime.GetUtcDateFromGasDayAndHour(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            gasDateTimeUtc.Should().Be(gasDayStartDateTimeUtc.AddHours(1));
//        }

//        [Test]
//        public void GetUtcDateFromGasDayAndHour_WithGasDayAndHour4WithOffset6_ReturnsNextCalendarDay3Utc()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDateTime = new DateTime(2020, 1, 1, 4, 0, 0, DateTimeKind.Unspecified);
//            var gasDayEndDateTimeUtc = gasDateTime.GetGasDayEndDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);

//            var gasDateTimeUtc = gasDateTime.GetUtcDateFromGasDayAndHour(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            gasDateTimeUtc.Should().Be(gasDayEndDateTimeUtc.AddHours(-1));
//        }

//        [Test]
//        public void GetUtcDateFromGasDayAndHour_WithGasDayHour559WithOffset6_ReturnsNextCalendarGasDay4Utc()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDateTime = new DateTime(2020, 1, 1, 5, 59, 0, DateTimeKind.Unspecified);
//            var gasDayEndDateTimeUtc = gasDateTime.GetGasDayEndDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);

//            var gasDateTimeUtc = gasDateTime.GetUtcDateFromGasDayAndHour(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            gasDateTimeUtc.Should().Be(gasDayEndDateTimeUtc.AddMinutes(59));
//        }

//        [Test]
//        public void GetGasDayAndHourFromUtcDate_WithGasDayEndCalendarDate_ReturnsGasDayAndHour()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
//            var gasDayCalendarEndDateTimeUtc = gasDay.GetGasDayEndDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            var gasDayAndHour = gasDayCalendarEndDateTimeUtc.GetGasDayAndHourFromUtcDate(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            gasDayAndHour.Should().Be(Convert.ToDateTime("01/01/2020 05:00:00"));
//        }

//        [Test]
//        public void GetGasDayAndHourFromUtcDate_WithGasDayStartCalendarDate_ReturnsGasDayAndHour()
//        {
//            var gasTimeZoneInfo = ValueTypes.GasTimeZoneInfoCet;
//            var gasDay = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
//            var gasDayCalendarEndDateTimeUtc = gasDay.GetGasDayStartDateTimeUtc(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            var gasDayAndHour = gasDayCalendarEndDateTimeUtc.GetGasDayAndHourFromUtcDate(gasTimeZoneInfo.GasDayOffset, gasTimeZoneInfo.TimeZoneName);
//            gasDayAndHour.Should().Be(Convert.ToDateTime("01/01/2020 06:00:00"));
//        }
//    }
//}

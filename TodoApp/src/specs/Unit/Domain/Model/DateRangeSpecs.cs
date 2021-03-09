using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Todo.Domain.Common;
using Todo.Domain.Model;

namespace Specs.Unit.Todo.Domain.Model
{
    public class DateRangeSpecs
    {
        [Test]
        public void Two_DateRanges_with_same_start_and_end_dates_should_be_Equal()
        {
            var firstEndDate = new DateTime(2011, 9, 5);
            var firstStartDate = firstEndDate.AddDays(-5);

            var firstDateRange = new DateRange(firstStartDate, firstEndDate);

            var secondEndDate = new DateTime(2011, 9, 5);
            var secondStartDate = secondEndDate.AddDays(-5);

            var secondDateRange = new DateRange(secondStartDate, secondEndDate);

            firstDateRange.Should().Be(secondDateRange);
        }

        [Test]
        public void Two_DateRanges_with_different_start_or_end_date_should_not_be_equal()
        {
            var firstEndDate = new DateTime(2011, 9, 5);
            var firstStartDate = firstEndDate.AddDays(-5);

            var firstDateRange = new DateRange(firstStartDate, firstEndDate);

            var secondEndDate = new DateTime(2011, 9, 5);
            var secondStartDate = firstEndDate.AddDays(-4);

            var secondDateRange = new DateRange(secondStartDate, secondEndDate);

            firstDateRange.Should().NotBe(secondDateRange);
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void StartDate_should_be_less_than_or_equal_to_EndDate(int additionalDays)
        {
            var endDate = new DateTime(2011, 9, 5);
            var startDate = endDate.AddDays(additionalDays);

            var sut = new DateRange(startDate, endDate);

            sut.StartDate.Should().Be(startDate);
            sut.EndDate.Should().Be(endDate);
        }

        [Test]
        public void Create_should_throw_if_StartDate_after_EndDate()
        {
            var endDate = new DateTime(2011, 9, 5);
            var startDate = endDate.AddDays(1);

            Action factory = () => new DateRange(startDate, endDate);

            factory.Should().Throw<ContractException>()
                .WithMessage("StartDate must be before or same as EndDate");
        }

        [Test]
        public void DateInDateRange_should_return_true_if_date_in_range_and_false_if_date_not_in_range()
        {
            var startDate = new DateTime(2011, 9, 5, 0, 0,0,  DateTimeKind.Utc);
            var endDate = startDate.AddDays(1);
            
            var sut = new DateRange(startDate, endDate);

            var date = new DateTime(2011, 9, 5,0,0,0, DateTimeKind.Utc).ToUniversalTime();

            sut.DateInDateRange(date).Should().Be(true);
            sut.DateInDateRange(date.AddDays(1)).Should().Be(true);
            sut.DateInDateRange(date.AddDays(2)).Should().Be(false);
            sut.DateInDateRange(date.AddDays(-1)).Should().Be(false);
        }

        [Test]
        public void DateRangeInDateRange_should_return_true_if_daterange_in_range_and_false_if_daterange_not_in_range()
        {
            var startDate = new DateTime(2011, 9, 5);
            var endDate = startDate.AddDays(3);

            var sut = new DateRange(startDate, endDate);

            sut.DateRangeInDateRange(new DateRange(startDate, endDate)).Should().BeTrue();
            sut.DateRangeInDateRange(new DateRange(startDate.AddDays(1), endDate.AddDays(-1)));
            sut.DateRangeInDateRange(new DateRange(startDate.AddDays(-1), endDate)).Should().BeFalse();
            sut.DateRangeInDateRange(new DateRange(startDate, endDate.AddDays(1))).Should().BeFalse();

        }

        [Test]
        public void Days_should_return_every_date_in_range()
        {
            var sut = new DateRange(new DateTime(2019, 4, 1), new DateTime(2019, 4, 3));

            sut.Days.Count.Should().Be(3);
            sut.Days.Should().BeEquivalentTo(new List<DateTime>
                {new DateTime(2019, 4, 1), new DateTime(2019, 4, 2), new DateTime(2019, 4, 3)});
        }

        [Test]
        public void Days_should_return_one_date_if_start_and_end_date_is_same()
        {
            var sut = new DateRange(new DateTime(2019, 4, 1), new DateTime(2019, 4, 1));

            sut.Days.Count.Should().Be(1);
            sut.Days.Should().BeEquivalentTo(new List<DateTime> {new DateTime(2019, 4, 1)});
        }

        [Test]
        public void TotalHours_should_return_hours_between_start_and_end_date()
        {
            var sut = new DateRange(new DateTime(2019, 4, 1,6,0,0), 
                new DateTime(2019, 4, 2, 9,0,0));

            sut.TotalHours.Should().Be(27);
        }
    }
}
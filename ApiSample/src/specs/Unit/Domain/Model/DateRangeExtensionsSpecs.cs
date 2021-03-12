using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ApiSample.Domain.Model;

namespace Specs.Unit.ApiSample.Domain.Model
{
    public class DateRangeExtensionsSpecs
    {
        [Test]
        public void GetDatesMissingFrom_should_return_list_of_dates_in_date_range_not_covered_by_list_of_date_ranges()
        {
            var dateRange = new DateRange(new DateTime(2012, 1, 3), new DateTime(2012, 1, 8));
            var sut = new List<DateRange>
            {
                // gap
                new DateRange(new DateTime(2012, 1, 4), new DateTime(2012, 1, 5)),
                // gap
                new DateRange(new DateTime(2012, 1, 7))
                // gap
            };

            var result = sut.GetDatesMissingFrom(dateRange);

            result.Count.Should().Be(3);
            result.Should().Contain(new DateTime(2012, 1, 3));
            result.Should().Contain(new DateTime(2012, 1, 6));
            result.Should().Contain(new DateTime(2012, 1, 8));
        }

        [Test]
        public void GetDatesMissingFrom_should_return_empty_list_if_no_dates_missing()
        {
            var dateRange = new DateRange(new DateTime(2012, 1, 4), new DateTime(2012, 1, 6));
            var sut = new List<DateRange>
            {
                new DateRange(new DateTime(2012, 1, 4)),
                new DateRange(new DateTime(2012, 1, 5)),
                new DateRange(new DateTime(2012, 1, 6))
            };

            var result = sut.GetDatesMissingFrom(dateRange);

            result.Count.Should().Be(0);
        }

        [Test]
        public void HaveDatesMissingFrom_should_return_true_if_dates_in_date_range_not_covered_by_list_of_date_ranges()
        {
            var dateRange = new DateRange(new DateTime(2012, 1, 3), new DateTime(2012, 1, 8));
            var sut = new List<DateRange>
            {
                // gap
                new DateRange(new DateTime(2012, 1, 4), new DateTime(2012, 1, 5)),
                // gap
                new DateRange(new DateTime(2012, 1, 7))
                // gap
            };

            var result = sut.HaveDatesMissingFrom(dateRange);

            result.Should().BeTrue();
        }

        [Test]
        public void HaveDatesMissingFrom_should_return_false_if_dates_in_date_range_not_covered_by_list_of_date_ranges()
        {
            var dateRange = new DateRange(new DateTime(2012, 1, 4), new DateTime(2012, 1, 6));
            var sut = new List<DateRange>
            {
                new DateRange(new DateTime(2012, 1, 4)),
                new DateRange(new DateTime(2012, 1, 5)),
                new DateRange(new DateTime(2012, 1, 6))
            };

            var result = sut.HaveDatesMissingFrom(dateRange);

            result.Should().BeFalse();
        }

        [Test]
        public void HaveOverlappingDates_should_return_true_if_any_constraint_dates_overlap()
        {
            var sut = new List<DateRange>
            {
                new DateRange(new DateTime(2012, 1, 12), new DateTime(2012, 1, 18)), // overlaps with previous date
                new DateRange(new DateTime(2012, 1, 11), new DateTime(2012, 1, 14)),
                new DateRange(new DateTime(2012, 1, 4), new DateTime(2012, 1, 6)),
                new DateRange(new DateTime(2012, 1, 7), new DateTime(2012, 1, 10)),
            };

            var result = sut.HaveOverlappingDates();

            result.Should().BeTrue();
        }

        [Test]
        public void HaveOverlappingDates_should_return_false_if_constraint_dates_dont_overlap()
        {
            var sut = new List<DateRange>
            {
                new DateRange(new DateTime(2012, 1, 4), new DateTime(2012, 1, 6)),
                new DateRange(new DateTime(2012, 1, 7), new DateTime(2012, 1, 10)),
                new DateRange(new DateTime(2012, 1, 11), new DateTime(2012, 1, 14)),
                new DateRange(new DateTime(2012, 1, 15), new DateTime(2012, 1, 18))
            };

            var result = sut.HaveOverlappingDates();

            result.Should().BeFalse();
        }

        [Test]
        public void HaveDatesOutOfRangeOf_should_return_true_if_any_start_or_end_date_in_list_is_out_of_range()
        {
            var dateRange = new DateRange(new DateTime(2012, 1, 3), new DateTime(2012, 1, 8));

            new List<DateRange> { new DateRange(new DateTime(2012, 1, 2), new DateTime(2012, 1, 4)) }
                .HaveDatesOutOfRangeOf(dateRange).Should().BeTrue();
            new List<DateRange> { new DateRange(new DateTime(2012, 1, 5), new DateTime(2012, 1, 9)) }
                .HaveDatesOutOfRangeOf(dateRange).Should().BeTrue();

            new List<DateRange> { new DateRange(new DateTime(2012, 1, 3), new DateTime(2012, 1, 8)) }
                .HaveDatesOutOfRangeOf(dateRange).Should().BeFalse();
            new List<DateRange> { new DateRange(new DateTime(2012, 1, 4), new DateTime(2012, 1, 7)) }
                .HaveDatesOutOfRangeOf(dateRange).Should().BeFalse();

        }
    }
}
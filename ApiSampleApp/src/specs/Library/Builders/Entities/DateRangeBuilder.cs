using System;
using ToDo.Domain.Model;
using TestStack.Dossier;

namespace Specs.Library.ToDo.Builders.Entities
{
    public class DateRangeBuilder : TestDataBuilder<DateRange, DateRangeBuilder>
    {
        public DateRangeBuilder()
        {
            Set(x => x.StartDate, new DateTime(2021, 4, 1));
            Set(x => x.EndDate, new DateTime(2022, 3, 31));
        }

        protected override DateRange BuildObject()
        {
            return new DateRange(
                Get(x => x.StartDate), Get(x => x.EndDate));
        }
    }
}
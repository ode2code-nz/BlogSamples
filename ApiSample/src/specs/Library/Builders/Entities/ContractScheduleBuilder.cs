using System;
using ApiSample.Domain.Model;
using ApiSample.Domain.Model.MasterTradingAgreements;
using TestStack.Dossier;
using TestStack.Dossier.Lists;
using static Specs.Library.ApiSample.Builders.Get;

namespace Specs.Library.ApiSample.Builders.Entities
{
    public class ContractScheduleBuilder : TestDataBuilder<ContractSchedule, ContractScheduleBuilder>
    {
        public ContractScheduleBuilder()
        {
            Set(x => x.Name, Any.Company.Name);
            Set(x => x.Type, ContractScheduleType.Mta);
            Set(x => x.Duration, new DateRangeBuilder());
            Set(x => x.Location,  StaticData.Location);
        }

        public static ListBuilder<ContractSchedule, ContractScheduleBuilder> CreateDefaultList(int size = 3, bool isNew = true)
        {
            var builder = ContractScheduleBuilder.CreateListOfSize(size)
                .All()
                .Set(x => x.Id, SequenceOf.Keys(size, isNew).Next);

            return builder.ListBuilder;
        }

        public virtual ContractScheduleBuilder WithDuration(DateTime startDate, DateTime endDate)
        {
            return Set(x => x.Duration, new DateRange(startDate, endDate));
        }

        protected override ContractSchedule BuildObject()
        {
            var entity = new ContractSchedule(
                Get(x => x.Name),
                Get(x => x.Type),
                Get(x => x.Duration),
                Get(x => x.Comments),
                Get(x => x.MasterTradingAgreement),
                Get(x => x.Location));

            return entity;
        }
    }
}
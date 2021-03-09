using System;
using System.Collections.Generic;
using System.Linq;
using ApiSample.Domain.Model;
using ApiSample.Domain.Model.MasterTradingAgreements;
using TestStack.Dossier;
using TestStack.Dossier.Lists;
using static Specs.Library.ApiSample.Builders.Get;

namespace Specs.Library.ApiSample.Builders.Entities
{
    public class MasterTradingAgreementBuilder : TestDataBuilder<MasterTradingAgreement, MasterTradingAgreementBuilder>
    {
        private List<ContractSchedule> _contractSchedules = new List<ContractSchedule>();

        public MasterTradingAgreementBuilder()
        {
            Set(x => x.Name, Any.Company.Name);
            Set(x => x.Type, MasterTradingAgreementType.Mra);
            Set(x => x.Duration, new DateRangeBuilder());
            Set(x => x.Counterparty, StaticData.Company);
            WithContractSchedules(ContractScheduleBuilder.CreateDefaultList());
        }

        public static ListBuilder<MasterTradingAgreement, MasterTradingAgreementBuilder> CreateDefaultList(int size = 3, bool isNew = true, bool persistRelatives = false)
        {
            var builder = CreateListOfSize(size)
                .All()
                .Set(x => x.Id, SequenceOf.Keys(size, isNew).Next)
                .Set(x => x.ContractSchedules, ContractScheduleBuilder.CreateDefaultList(size, isNew).BuildList().ToList());

            return builder.ListBuilder;
        }

        public virtual MasterTradingAgreementBuilder WithContractSchedules(List<ContractSchedule> contractSchedules)
        {
            _contractSchedules = contractSchedules;
            return this;
        }

        public virtual MasterTradingAgreementBuilder WithDuration(DateTime startDate, DateTime endDate)
        {
            return Set(x => x.Duration, new DateRange(startDate, endDate));
        }

        protected override MasterTradingAgreement BuildObject()
        {
            var entity = new MasterTradingAgreement(
                Get(x => x.Name),
                Get(x => x.Type),
                Get(x => x.Duration),
                Get(x => x.Counterparty),
                Get(x => x.Comments));

            foreach (var schedule in _contractSchedules)
            {
                entity.AddContractSchedule(schedule);
            }

            return entity;
        }

    }
}

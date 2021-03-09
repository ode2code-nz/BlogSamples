using System;
using ApiSample.Api.Common.Mappings;
using ApiSample.Domain.Model.MasterTradingAgreements;
using ApiSample.SharedModels.v1.MasterTradingAgreements;

namespace ApiSample.Api.Features.v1.MasterTradingAgreements
{
    public class ContractScheduleDto : IMapFrom<ContractSchedule>, IMapToAndFrom<ContractScheduleResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Type { get; set; }

        //public int MasterTradingAgreementId { get; set; }

        public DateTime DurationStartDate { get; set; }

        public DateTime DurationEndDate { get; set; }

        public string Comments { get; set; }

        public int LocationId { get; set; }
    }
}
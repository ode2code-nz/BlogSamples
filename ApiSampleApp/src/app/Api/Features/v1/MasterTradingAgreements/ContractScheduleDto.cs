using System;
using ToDo.Api.Common.Mappings;
using ToDo.Domain.Model.MasterTradingAgreements;
using ToDo.SharedModels.v1.MasterTradingAgreements;

namespace ToDo.Api.Features.v1.MasterTradingAgreements
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
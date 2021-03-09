using System;
using System.Collections.Generic;
using AutoMapper;
using Todo.Api.Common.Mappings;
using Todo.Domain.Model.MasterTradingAgreements;
using Todo.SharedModels.v1.MasterTradingAgreements;

namespace Todo.Api.Features.v1.MasterTradingAgreements
{
    public class MasterTradingAgreementDto : IMapFrom<MasterTradingAgreement>, IMapToAndFrom<MasterTradingAgreementResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public List<ContractScheduleDto> ContractSchedules { get; set; }

        public DateTime DurationStartDate { get; set; }

        public DateTime DurationEndDate { get; set; }

        public string Comments { get; set; }

        public int CounterpartyId { get; set; }

        public bool IgnoreWarnings { get; set; } = false;

        public void MapFrom(Profile profile)
        {
            profile.CreateMap<MasterTradingAgreement, MasterTradingAgreementDto>()
                .ForMember(d => d.IgnoreWarnings,
                    opt => opt.Ignore());
        }
    }
}
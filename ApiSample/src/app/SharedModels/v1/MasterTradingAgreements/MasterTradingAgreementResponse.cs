using System;
using System.Collections.Generic;

namespace ApiSample.SharedModels.v1.MasterTradingAgreements
{
    public class MasterTradingAgreementResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public List<ContractScheduleResponse> ContractSchedules { get; set; }

        public DateTime DurationStartDate { get; set; }

        public DateTime DurationEndDate { get; set; }

        public string Comments { get; set; }

        public int CounterpartyId { get; set; }

        public bool IgnoreWarnings { get; set; } = false;

    }
}
using System;

namespace ToDo.SharedModels.v1.MasterTradingAgreements
{
    public class ContractScheduleResponse
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
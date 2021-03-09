using System.Collections.Generic;
using ToDo.Domain.Common;
using ToDo.Domain.Model.StaticData;

namespace ToDo.Domain.Model.MasterTradingAgreements
{
    public class MasterTradingAgreement : AggregateRoot
    {
        protected MasterTradingAgreement()
        {
        }

        public MasterTradingAgreement(string name, MasterTradingAgreementType type, DateRange duration,
            Company counterparty, string comments)
        {
            Guard.Against(!string.IsNullOrEmpty(name), "Name is required");

            Name = name;
            Type = type;
            Duration = duration;
            Counterparty = counterparty;
            Comments = comments;
        }

        public string Name { get; private set; }
        public MasterTradingAgreementType Type { get; private set; }
        public DateRange Duration { get; private set; }
        public string Comments { get; private set; }

        public Company Counterparty { get; private set; }

        public void Update(string name, MasterTradingAgreementType type, DateRange duration,
            Company counterparty, string comments)
        {
            Guard.Against(!string.IsNullOrEmpty(name), "Name is required");

            Name = name;
            Type = type;
            Duration = duration;
            Counterparty = counterparty;
            Comments = comments;
        }

        #region ContractSchedules

        private readonly List<ContractSchedule> _contractSchedules = new List<ContractSchedule>();
        public IReadOnlyList<ContractSchedule> ContractSchedules => _contractSchedules;

        public void AddContractSchedule(ContractSchedule contractSchedule)
        {
            _contractSchedules.Add(contractSchedule);
        }

        public void RemoveContractSchedule(ContractSchedule contractSchedule)
        {
            _contractSchedules.Remove(contractSchedule);
        }

        #endregion
    }
}
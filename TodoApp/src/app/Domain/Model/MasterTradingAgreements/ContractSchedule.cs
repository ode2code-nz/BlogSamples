using Todo.Domain.Common;
using Todo.Domain.Model.StaticData;

namespace Todo.Domain.Model.MasterTradingAgreements
{
    public class ContractSchedule : Entity
    {
        protected ContractSchedule()
        { }

        public ContractSchedule(string name, ContractScheduleType type, DateRange duration,
            string comments, MasterTradingAgreement agreement, Location location)
        {
            Guard.Against(!string.IsNullOrEmpty(name), "Name is required");

            Name = name;
            Type = type;
            Duration = duration;
            Comments = comments;
            MasterTradingAgreement = agreement;
            Location = location;
        }

        public string Name { get; private set; }
        public ContractScheduleType Type { get; private set; }
        public DateRange Duration { get; private set; }

        public string Comments { get; private set; }

        public MasterTradingAgreement MasterTradingAgreement { get; private set; }

        public Location Location { get; private set; }

        public void Update(string name, ContractScheduleType type, DateRange duration,
            string comments, MasterTradingAgreement agreement, Location location)
        {
            Guard.Against(!string.IsNullOrEmpty(name), "Name is required");

            Name = name;
            Type = type;
            Duration = duration;
            Comments = comments;
            MasterTradingAgreement = agreement;
            Location = location;
        }
    }
}
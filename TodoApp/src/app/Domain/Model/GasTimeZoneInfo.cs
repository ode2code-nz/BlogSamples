using System;
using System.Collections.Generic;
using Todo.Domain.Common;
using Todo.Domain.Core;

namespace Todo.Domain.Model
{
    public class GasTimeZoneInfo: ValueObject
    {
        public GasTimeZoneInfo(Enums.GasTimeZoneInfo GasTimeZoneInfoId, int gasDayOffset, string timeZoneName, string description, string shortCode, string hexIndex, int utcOffset, string tzCode)
        {
            Id = (int) GasTimeZoneInfoId;
            GasDayOffset = gasDayOffset;
            TimeZoneName = timeZoneName;
            Description = description;
            ShortCode = shortCode;
            HexIndex = hexIndex;
            UtcOffset = utcOffset;
            TzCode = tzCode;
        }

        public Enums.GasTimeZoneInfo GasTimeZoneInfoId
        {
            get => (Enums.GasTimeZoneInfo)Id;
            set => Id = (int)value;
        }

        public int Id { get; private set; }

        public int GasDayOffset { get; private set; }
        public string TimeZoneName { get; private set; }
        public string Description { get; private set; }
        public string Code => TimeZoneName;
        public string ShortCode { get; private set; }
        public string HexIndex { get; private set; }
        public int UtcOffset { get; private set; }
        public Enums.GasTimeZoneInfo TimeZoneCode => (Enums.GasTimeZoneInfo)this.Id;

        //https://en.wikipedia.org/wiki/List_of_tz_database_time_zones
        public string TzCode { get; set; }

        public DateTime? GetDayAheadDeadline(TimeSpan? dayAheadCutOffTime)
        {
            if (dayAheadCutOffTime == null)
            {
                return null;
            }
            var gasDay = SystemTime.UtcNow().Date;
            return new DateTime(gasDay.Year, gasDay.Month, gasDay.Day, dayAheadCutOffTime.Value.Hours, dayAheadCutOffTime.Value.Minutes, 0, DateTimeKind.Unspecified).ConvertToUtc(TimeZoneName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}

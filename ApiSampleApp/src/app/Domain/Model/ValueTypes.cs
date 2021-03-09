namespace ApiSample.Domain.Model
{
    public static class ValueTypes
    {
        public static GasTimeZoneInfo GasTimeZoneInfoGmt => new GasTimeZoneInfo(Enums.GasTimeZoneInfo.GMT, 5, "GMT Standard Time", "(GMT) Greenwich Mean Time : Dublin, Edinburgh, Lisbon, London", "GMT", "55", 0, "Europe/London");
        public static GasTimeZoneInfo GasTimeZoneInfoCet => new GasTimeZoneInfo(Enums.GasTimeZoneInfo.GMT, 6, "Central Europe Standard Time", "(GMT+01:00) Belgrade, Bratislava, Budapest, Ljubljana, Prague", "CET", "5F", 1, "Europe/Paris");
    }
}

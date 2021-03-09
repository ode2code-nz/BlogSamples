namespace Todo.Infrastructure
{
    public class AppSettings
    {
        public int StaticDataCacheExpiryMins { get; set; }

        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}

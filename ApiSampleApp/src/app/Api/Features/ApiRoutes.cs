namespace ApiSample.Api.Features
{
    public static class ApiRoutes
    {
        private const string Root = "/api";      // E.g. "api"

        private const string Version = "/v1";    // E.g. "/v1";

        public const string Base = Root + Version;

        public static class ToDo
        {
            public const string Get = Base + "/ToDo/{id}";
            public const string GetAll = Base + "/ToDo";
            public const string Create = Base + "/ToDo";
            public const string Update = Base + "/ToDo";
            public const string Delete = Base + "/ToDo/{id}";

            public static string GetFor(int id) => Get.Replace("{id}", id.ToString());
            public static string DeleteFor(int id) => Delete.Replace("{id}", id.ToString());
        }

        public static class MasterTradingAgreement
        {
            public const string Get = Base + "/MasterTradingAgreement/{id}";
            public const string GetAll = Base + "/MasterTradingAgreement";
            public const string Create = Base + "/MasterTradingAgreement";
            public const string Update = Base + "/MasterTradingAgreement";
            public const string Delete = Base + "/MasterTradingAgreement/{id}";

            public static string GetFor(int id) => Get.Replace("{id}", id.ToString());
            public static string DeleteFor(int id) => Delete.Replace("{id}", id.ToString());
        }

        public static class StaticData
        {
            public const string GetCompanies = Base + "/staticdata/companies";
            public const string GetLocations = Base + "/staticdata/locations";
        }

        public static class Secured
        {
            public const string Get = Base + "/secured";
            public const string Post = Base + "/secured";
        }
    }
}
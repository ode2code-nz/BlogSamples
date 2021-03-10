using System;
using System.Net.Http.Headers;
using Specify;
using Specify.Configuration;
using Specs.Library.ApiSample.Builders;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Library.ApiSample.Identity
{
    public class ResetDefaultUser : IPerScenarioAction
    {
        public void Before<TSut>(IScenario<TSut> scenario) where TSut : class
        {
            var typed = scenario as IScenario<AsyncApiDriver>;
            if (typed == null)
            {
                return;
            }

            var defaultUser = Get.User.Administrator;
            var defaultUserToken = defaultUser.Token;

            typed.SUT.
            Client.DefaultRequestHeaders
            .Authorization = new AuthenticationHeaderValue(
                "Bearer", defaultUserToken);

        }

        public void After()
        {
        }

        public bool ShouldExecute(Type type)
        {
            return true;
        }

        public int Order { get; set; }
    }
}
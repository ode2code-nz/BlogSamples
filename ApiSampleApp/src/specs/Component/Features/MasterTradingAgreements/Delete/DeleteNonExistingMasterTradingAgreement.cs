using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.MasterTradingAgreements.Delete
{
    public class DeleteNonExistingMasterTradingAgreement : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse _result;

        public async Task When_I_attempt_to_delete_a_MasterTradingAgreement_that_does_not_exist()
        {
            _result = await SUT.DeleteAsync(ApiRoutes.MasterTradingAgreement.DeleteFor(99));
        }

        public void Then_I_should_receive_a_not_found_warning()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
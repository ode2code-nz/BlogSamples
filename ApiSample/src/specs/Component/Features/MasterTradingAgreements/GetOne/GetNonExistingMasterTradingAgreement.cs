using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using ApiSample.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.MasterTradingAgreements.GetOne
{
    public class GetNonExistingMasterTradingAgreement : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse<MasterTradingAgreementResponse> _result;

        public async Task When_I_attempt_to_view_a_MasterTradingAgreement_that_does_not_exist()
        {
            _result = await SUT.GetAsync<MasterTradingAgreementResponse>(ApiRoutes.MasterTradingAgreement.GetFor(99));
        }

        public void Then_I_should_receive_a_not_found_warning()
        {
            _result.Model.Should().BeNull();
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
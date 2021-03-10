using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using ApiSample.Domain.Model.MasterTradingAgreements;
using ApiSample.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.ApiSample.Builders.Entities;
using Specs.Library.ApiSample.Data;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.MasterTradingAgreements.GetOne
{
    public class GetExistingMasterTradingAgreement : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse<MasterTradingAgreementResponse> _result;
        private MasterTradingAgreement _existingItem;

        public void Given_I_have_created_a_MasterTradingAgreement()
        {
            _existingItem = new MasterTradingAgreementBuilder().Persist();
        }

        public async Task When_I_attempt_to_view_it()
        {
             _result = await SUT.GetAsync<MasterTradingAgreementResponse>(ApiRoutes.MasterTradingAgreement.GetFor(_existingItem.Id));
        }

        public void Then_I_should_see_the_MasterTradingAgreement()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
            _result.Model.Id.Should().Be(_existingItem.Id);
        }
    }
}

using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using ApiSample.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.MasterTradingAgreements.GetAll
{
    public class GetAllMasterTradingAgreementsWhenNone : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse<List<MasterTradingAgreementResponse>> _result;

        public void Given_I_do_not_have_any_MasterTradingAgreement()
        {
        }

        public async Task When_I_view_my_list()
        {
            _result = await SUT.GetAllAsync<MasterTradingAgreementResponse>(ApiRoutes.MasterTradingAgreement.GetAll);
        }

        public void Then_I_should_see_an_empty_list()
        {
            _result.Model.Should().HaveCount(0);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
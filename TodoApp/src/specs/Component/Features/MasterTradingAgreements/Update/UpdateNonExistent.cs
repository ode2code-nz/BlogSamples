using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Mapster;
using Todo.Api.Features;
using Todo.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.MasterTradingAgreements.Update
{
    public class UpdateNonExistent : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse _result;
        private UpdateMasterTradingAgreementRequest _request;

        public void Given_I_am_trying_to_edit_an_item_that_does_not_exist()
        {
            _request = new MasterTradingAgreementBuilder().Build()
                .Adapt<UpdateMasterTradingAgreementRequest>();
        }

        public async Task When_I_attempt_to_apply_changes_to_it()
        {
            _result = await SUT.PutAsync(ApiRoutes.MasterTradingAgreement.Update, _request);
        }

        public void Then_I_should_be_warned_that_the_item_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
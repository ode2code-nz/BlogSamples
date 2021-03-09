using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using ToDo.Domain.Model.MasterTradingAgreements;
using ToDo.SharedModels.v1.Responses;
using ToDo.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.ToDo.Drivers.Api;
using Specs.Library.ToDo.Extensions;
using TestStack.Dossier;

namespace Specs.Component.ToDo.Features.MasterTradingAgreements.Create
{
    public class CreateInvalidMasterTradingAgreement : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private CreateMasterTradingAgreementRequest _request;

        public void Given_I_have_composed_an_invalid_new_MasterTradingAgreement()
        {
            _request = Builder<CreateMasterTradingAgreementRequest>.CreateNew()
                .Set(x => x.Name, string.Empty);
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.MasterTradingAgreement.Create, _request);
        }

        public void Then_the_new_MasterTradingAgreement_should_not_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            Db.Set<MasterTradingAgreement>().Should().HaveCount(0);
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.ShouldContainErrors("'Name' must not be empty.");
        }
    }
}
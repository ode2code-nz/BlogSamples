using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Mapster;
using Todo.Api.Features;
using Todo.Domain.Model.MasterTradingAgreements;
using Todo.SharedModels.v1.Responses;
using Todo.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.MasterTradingAgreements.Create
{
    public class CreateValidMasterTradingAgreement : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private CreateMasterTradingAgreementRequest _request;

        public void Given_I_have_composed_a_valid_new_MasterTradingAgreement()
        {
            _request = new MasterTradingAgreementBuilder().Build()
                .Adapt<CreateMasterTradingAgreementRequest>();
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.MasterTradingAgreement.Create, _request);
        }

        public void Then_the_new_MasterTradingAgreement_should_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Created);
            _result.Model.NewIds.First().Should().BeGreaterThan(0);

            Db.Find<MasterTradingAgreement>(_result.Model.NewIds.First()).Should().NotBeNull();
        }

        public void AndThen_the_related_ContractSchedules_should_be_created()
        {
            Db.Set<ContractSchedule>().Should().HaveCount(3);
        }

        public void AndThen_the_link_to_the_new_MasterTradingAgreement_should_be_provided()
        {
            _result.Headers.Location.AbsolutePath.Should().Be(ApiRoutes.MasterTradingAgreement.GetFor(_result.Model.NewIds.First()));
        }
    }
}
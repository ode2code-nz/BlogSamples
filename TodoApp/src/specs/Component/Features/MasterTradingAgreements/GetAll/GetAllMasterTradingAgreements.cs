using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Todo.Api.Features;
using Todo.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Data;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.MasterTradingAgreements.GetAll
{
    public class GetAllMasterTradingAgreements : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse<List<MasterTradingAgreementResponse>> _result;

        public void Given_I_have_created_a_list_of_things_to_do()
        {
            MasterTradingAgreementBuilder.CreateDefaultList().Persist();
        }

        public async Task When_I_view_my_list()
        {
             _result = await SUT.GetAllAsync<MasterTradingAgreementResponse>(ApiRoutes.MasterTradingAgreement.GetAll);
        }

        public void Then_I_should_see_all_the_things_I_have_to_do()
        {
            _result.Model.Should().HaveCount(3);
            _result.Model.SelectMany(x => x.ContractSchedules).Should().HaveCount(9);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

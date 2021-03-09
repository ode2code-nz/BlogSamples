using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using ToDo.SharedModels.v1.MasterTradingAgreements;
using Specs.Library.ToDo.Builders.Entities;
using Specs.Library.ToDo.Data;
using Specs.Library.ToDo.Drivers.Api;

namespace Specs.Component.ToDo.Features.MasterTradingAgreements.GetAll
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

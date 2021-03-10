using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using ApiSample.Api.Features;
using ApiSample.Domain.Model.MasterTradingAgreements;
using Specs.Library.ApiSample.Builders.Entities;
using Specs.Library.ApiSample.Data;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.MasterTradingAgreements.Delete
{
    public class ValidMasterTradingAgreementDelete : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse _result;
        private MasterTradingAgreement _existingItem;

        public void Given_I_have_an_existing_MasterTradingAgreement()
        {
            _existingItem = new MasterTradingAgreementBuilder().Persist();
        }

        public async Task When_I_attempt_to_delete_it()
        {
            _result = await SUT.DeleteWithCheckAsync(ApiRoutes.MasterTradingAgreement.DeleteFor(_existingItem.Id));
        }

        public void Then_the_MasterTradingAgreement_should_be_deleted()
        {
            Db.Set<MasterTradingAgreement>().Should().HaveCount(0);
            _result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Mapster;
using Todo.Api.Features;
using Todo.Domain.Model.MasterTradingAgreements;
using Todo.SharedModels.v1.MasterTradingAgreements;
using Specify;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Data;
using Specs.Library.Todo.Drivers.Api;
using Specs.Library.Todo.Extensions;

namespace Specs.Component.Todo.Features.MasterTradingAgreements.Update
{
    public class InvalidUpdate : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse _result;
        private MasterTradingAgreement _existingItem;
        private UpdateMasterTradingAgreementRequest _request;

        public void Given_I_have_made_invalid_changes_to_an_existing_item()
        {
            _existingItem = new MasterTradingAgreementBuilder().Persist();

            _request = _existingItem
                .Adapt<UpdateMasterTradingAgreementRequest>()
                .With(x => x.Id = _existingItem.Id)
                .With(x => x.Name = string.Empty)
                .With(x => x.Comments = "Updated");
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.MasterTradingAgreement.Update, _request);
        }

        public void Then_the_changes_should_not_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            Db.Find<MasterTradingAgreement>(_existingItem.Id)
                .Comments.Should().NotBe("Updated");
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.ShouldContainErrors("'Name' must not be empty.");
        }

    }
}
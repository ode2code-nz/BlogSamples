using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Features;
using ToDo.Domain.Model.MasterTradingAgreements;
using ToDo.SharedModels.v1.MasterTradingAgreements;
using Specify;
using Specs.Library.ToDo.Builders.Entities;
using Specs.Library.ToDo.Data;
using Specs.Library.ToDo.Drivers.Api;

namespace Specs.Component.ToDo.Features.MasterTradingAgreements.Update
{
    public class ValidUpdate : ScenarioFor<AsyncApiDriver, MasterTradingAgreementStory>
    {
        private ApiResponse _result;
        private MasterTradingAgreement _existingItem;
        private UpdateMasterTradingAgreementRequest _request;
        private const string UpdatedValue = "Updated";

        public void Given_I_have_made_valid_changes_to_an_existing_ToDo()
        {
            _existingItem = new MasterTradingAgreementBuilder().Persist();
            _request = _existingItem
                .Adapt<UpdateMasterTradingAgreementRequest>()
                .With(x => x.Comments = UpdatedValue);
            _request.ContractSchedules[0].Comments = UpdatedValue;
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.MasterTradingAgreement.Update, _request);
        }

        public void Then_the_changes_should_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            var entity = Db.Set<MasterTradingAgreement>().Include(x => x.ContractSchedules)
                .Single(x => x.Id == _existingItem.Id);
            entity.Comments.Should().Be(UpdatedValue);
            entity.ContractSchedules[0].Comments.Should().Be(UpdatedValue);
        }
    }
}
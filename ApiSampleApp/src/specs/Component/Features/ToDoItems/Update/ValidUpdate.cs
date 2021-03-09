using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using ApiSample.Api.Features.v1.ToDoItems;
using ApiSample.Domain.Model.ToDos;
using Specs.Library.ApiSample.Builders.Entities;
using Specs.Library.ApiSample.Data;
using Specs.Library.ApiSample.Drivers.Api;
using TestStack.Dossier;

namespace Specs.Component.ApiSample.Features.ToDoItems.Update
{
    public class ValidUpdate : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse _result;
        private ToDoItem _existingItem;
        private UpdateToDoItemCommand _updates;

        public void Given_I_have_made_valid_changes_to_an_existing_ToDo()
        {
            _existingItem = new ToDoItemBuilder().Persist();
            _updates = Builder<UpdateToDoItemCommand>.CreateNew()
                .Set(x => x.Description, "Updated")
                .Set(x => x.Id, _existingItem.Id);
        }

        public async Task When_I_attempt_to_apply_the_changes()
        {
            _result = await SUT.PutAsync(ApiRoutes.ToDo.Update, _updates);
        }

        public void Then_the_changes_should_be_saved()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NoContent);
            Db.Find<ToDoItem>(_existingItem.Id)
                .Description.Should().Be("Updated");
        }
    }
}
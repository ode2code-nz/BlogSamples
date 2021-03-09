using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using ToDo.Api.Features.v1.ToDoItems;
using ToDo.Domain.Model.ToDos;
using Specs.Library.ToDo.Builders.Entities;
using Specs.Library.ToDo.Data;
using Specs.Library.ToDo.Drivers.Api;
using TestStack.Dossier;

namespace Specs.Component.ToDo.Features.ToDoItems.Update
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
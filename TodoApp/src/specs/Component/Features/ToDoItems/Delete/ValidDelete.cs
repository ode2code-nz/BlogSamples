using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Todo.Api.Features;
using Todo.Domain.Model.ToDos;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Data;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.ToDoItems.Delete
{
    public class ValidDelete : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse _result;
        private ToDoItem _existingItem;

        public void Given_I_have_an_existing_todo()
        {
            _existingItem = new ToDoItemBuilder().Persist();
        }

        public async Task When_I_attempt_to_delete_it()
        {
            _result = await SUT.DeleteWithCheckAsync(ApiRoutes.ToDo.DeleteFor(_existingItem.Id));
        }

        public void Then_the_todo_should_be_deleted()
        {
            Db.Set<ToDoItem>().Should().HaveCount(0);
            _result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }
    }
}
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Todo.Api.Features;
using Todo.Domain.Model.ToDos;
using Todo.SharedModels.v1.ToDoItems;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Data;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.ToDoItems.GetOne
{
    public class GetExistingToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<ToDoItemResponse> _result;
        private ToDoItem _existingItem;

        public void Given_I_have_created_a_to_do()
        {
            _existingItem = new ToDoItemBuilder().Persist();
        }

        public async Task When_I_attempt_to_view_it()
        {
             _result = await SUT.GetAsync<ToDoItemResponse>(ApiRoutes.ToDo.GetFor(_existingItem.Id));
        }

        public void Then_I_should_see_all_the_things_I_have_to_do()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
            _result.Model.Id.Should().Be(_existingItem.Id);
        }
    }
}

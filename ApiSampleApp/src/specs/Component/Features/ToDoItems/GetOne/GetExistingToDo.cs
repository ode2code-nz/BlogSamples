using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using ToDo.Domain.Model.ToDos;
using ToDo.SharedModels.v1.ToDoItems;
using Specs.Library.ToDo.Builders.Entities;
using Specs.Library.ToDo.Data;
using Specs.Library.ToDo.Drivers.Api;

namespace Specs.Component.ToDo.Features.ToDoItems.GetOne
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

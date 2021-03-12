using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using ApiSample.Domain.Model.ToDos;
using ApiSample.SharedModels.v1.ToDoItems;
using Specs.Library.ApiSample.Builders.Entities;
using Specs.Library.ApiSample.Data;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.ToDoItems.GetOne
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

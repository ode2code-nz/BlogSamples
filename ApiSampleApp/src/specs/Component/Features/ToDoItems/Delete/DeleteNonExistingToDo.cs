using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using Specs.Library.ToDo.Drivers.Api;

namespace Specs.Component.ToDo.Features.ToDoItems.Delete
{
    public class DeleteNonExistingToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse _result;

        public async Task When_I_attempt_to_delete_a_ToDo_that_does_not_exist()
        {
            _result = await SUT.DeleteAsync(ApiRoutes.ToDo.DeleteFor(99));
        }

        public void Then_I_should_receive_a_not_found_warning()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
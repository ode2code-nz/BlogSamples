using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using ToDo.Api.Features.v1.ToDoItems;
using Specs.Library.ToDo.Builders;
using Specs.Library.ToDo.Drivers.Api;

namespace Specs.Component.ToDo.Features.ToDoItems.Update
{
    public class UpdateNonExistent : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse _result;
        private UpdateToDoItemCommand _updates;

        public void Given_I_am_trying_to_edit_a_ToDo_that_does_not_exist()
        {
            _updates = Get.InstanceOf<UpdateToDoItemCommand>();
        }

        public async Task When_I_attempt_to_apply_changes_to_it()
        {
            _result = await SUT.PutAsync(ApiRoutes.ToDo.Update, _updates);
        }

        public void Then_I_should_be_warned_that_the_ToDo_does_not_exist()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
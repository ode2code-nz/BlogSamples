using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using ToDo.Domain.Model.ToDos;
using ToDo.SharedModels.v1.Responses;
using ToDo.SharedModels.v1.ToDoItems;
using Specs.Library.ToDo.Drivers.Api;
using TestStack.Dossier;

namespace Specs.Component.ToDo.Features.ToDoItems.Create
{
    public class CreateValidToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private CreateToDoItemRequest _request;

        public void Given_I_have_composed_a_valid_new_ToDo()
        {
            _request = Builder<CreateToDoItemRequest>.CreateNew();
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.ToDo.Create, _request);
        }

        public void Then_the_new_ToDo_should_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Created);
            _result.Model.NewIds.First().Should().BeGreaterThan(0);
            Db.Find<ToDoItem>(_result.Model.NewIds.First()).Should().NotBeNull();
        }

        public void AndThen_the_link_to_the_new_ToDod_should_be_provided()
        {
            _result.Headers.Location.AbsolutePath.Should().Be(ApiRoutes.ToDo.GetFor(_result.Model.NewIds.First()));
        }
    }
}
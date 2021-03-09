using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Todo.Api.Features;
using Todo.Domain.Model.ToDos;
using Todo.SharedModels.v1.Responses;
using Todo.SharedModels.v1.ToDoItems;
using Specs.Library.Todo.Drivers.Api;
using TestStack.Dossier;

namespace Specs.Component.Todo.Features.ToDoItems.Create
{
    public class CreateValidToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private CreateTodoItemRequest _request;

        public void Given_I_have_composed_a_valid_new_ToDo()
        {
            _request = Builder<CreateTodoItemRequest>.CreateNew();
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.ToDo.Create, _request);
        }

        public void Then_the_new_todo_should_be_created()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Created);
            _result.Model.NewIds.First().Should().BeGreaterThan(0);
            Db.Find<ToDoItem>(_result.Model.NewIds.First()).Should().NotBeNull();
        }

        public void AndThen_the_link_to_the_new_todod_should_be_provided()
        {
            _result.Headers.Location.AbsolutePath.Should().Be(ApiRoutes.ToDo.GetFor(_result.Model.NewIds.First()));
        }
    }
}
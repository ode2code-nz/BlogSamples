using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using ToDo.Api.Features.v1.ToDoItems;
using ToDo.Domain.Model.ToDos;
using ToDo.SharedModels.v1.Responses;
using Specs.Library.ToDo.Drivers.Api;
using Specs.Library.ToDo.Extensions;

namespace Specs.Component.ToDo.Features.ToDoItems.Create
{
    public class CreateInvalidToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<RecordsCreatedResponse> _result;
        private CreateToDoItemCommand _command;

        public void Given_I_have_composed_an_invalid_new_ToDo()
        {
            _command = new CreateToDoItemCommand();
        }

        public async Task When_I_attempt_to_create_it()
        {
            _result = await SUT.PostAsync<RecordsCreatedResponse>(ApiRoutes.ToDo.Create, _command);
        }

        public void Then_the_new_ToDo_is_not_created()
        {
            Db.Set<ToDoItem>().Should().HaveCount(0);
        }

        public void And_then_I_should_be_told_the_reasons_why()
        {
            _result.ShouldContainErrors(
                "'Title' must not be empty.",
                "'Description' must not be empty.");
        }
    }
}
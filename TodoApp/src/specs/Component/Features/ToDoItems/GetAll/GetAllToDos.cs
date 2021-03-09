using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Todo.Api.Features;
using Todo.SharedModels.v1.ToDoItems;
using Specs.Library.Todo.Builders.Entities;
using Specs.Library.Todo.Data;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.ToDoItems.GetAll
{
    public class GetAllToDos : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<List<ToDoItemResponse>> _result;

        public void Given_I_have_created_a_list_of_things_to_do()
        {
            ToDoItemBuilder.CreateDefaultList().Persist();
        }

        public async Task When_I_view_my_list()
        {
             _result = await SUT.GetAllAsync<ToDoItemResponse>(ApiRoutes.ToDo.GetAll);
        }

        public void Then_I_should_see_all_the_things_I_have_to_do()
        {
            _result.Model.Count.Should().Be(3);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

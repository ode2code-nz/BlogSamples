using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using ApiSample.SharedModels.v1.ToDoItems;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.ToDoItems.GetAll
{
    public class GetAllToDosWhenNone : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<List<ToDoItemResponse>> _result;

        public void Given_I_do_not_have_any_things_to_do()
        {
        }

        public async Task When_I_view_my_to_do_list()
        {
            _result = await SUT.GetAllAsync<ToDoItemResponse>(ApiRoutes.ToDo.GetAll);
        }

        public void Then_I_should_see_an_empty_list()
        {
            _result.Model.Count.Should().Be(0);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
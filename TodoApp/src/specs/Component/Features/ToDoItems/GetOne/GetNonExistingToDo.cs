﻿using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Todo.Api.Features;
using Todo.SharedModels.v1.ToDoItems;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.ToDoItems.GetOne
{
    public class GetNonExistingToDo : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<ToDoItemResponse> _result;

        public async Task When_I_attempt_to_view_a_todo_that_does_not_exist()
        {
            _result = await SUT.GetAsync<ToDoItemResponse>(ApiRoutes.ToDo.GetFor(99));
        }

        public void Then_I_should_receive_a_not_found_warning()
        {
            //_result.Model.Should().BeNull();
            _result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Todo.Api.Features;
using Specs.Component.Todo.Features.ToDoItems;
using Specs.Library.Todo.Builders;
using Specs.Library.Todo.Drivers.Api;

namespace Specs.Component.Todo.Features.Secured
{
    public class UnauthorizedRequest : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<string> _result;

        public void Given_I_am_authenticated_but_not_in_the_correct_role()
        {
            SUT.RunAsIdentity = Get.User.Moderator;
        }

        public async Task When_I_attempt_to_view_an_endpoint_that_requires_that_role()
        {
            _result = await SUT.PostAsync<string>(ApiRoutes.Secured.Post, null);
        }

        public void Then_I_should_not_have_access_to_the_endpoint()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.Forbidden);
        }
    }

}

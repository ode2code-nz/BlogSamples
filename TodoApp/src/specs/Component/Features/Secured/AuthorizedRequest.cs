using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ToDo.Api.Features;
using Specs.Component.ToDo.Features.ToDoItems;
using Specs.Library.ToDo.Builders;
using Specs.Library.ToDo.Drivers.Api;

namespace Specs.Component.ToDo.Features.Secured
{
    public class AuthorizedRequest : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<string> _result;

        public void Given_I_am_authenticated_and_in_the_correct_role()
        {
            SUT.RunAsIdentity = Get.User.Administrator;
        }

        public async Task When_I_attempt_to_view_an_endpoint_that_requires_that_role()
        {
            _result = await SUT.PostAsync<string>(ApiRoutes.Secured.Post, null);
        }

        public void Then_I_should_have_access_to_the_endpoint()
        {
            _result.Model.Should().Be("This Secured Data is available only for Administrators.");
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
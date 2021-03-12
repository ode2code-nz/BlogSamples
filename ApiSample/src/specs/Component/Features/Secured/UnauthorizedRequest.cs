using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using Specs.Component.ApiSample.Features.ToDoItems;
using Specs.Library.ApiSample.Builders;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.Secured
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

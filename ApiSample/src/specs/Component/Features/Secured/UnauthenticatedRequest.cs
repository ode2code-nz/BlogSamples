using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using Specs.Component.ApiSample.Features.ToDoItems;
using Specs.Library.ApiSample.Builders;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.Secured
{
    public class UnauthenticatedRequest : ScenarioFor<AsyncApiDriver, ToDoStory>
    {
        private ApiResponse<string> _result;

        public void Given_I_am_not_authenticated()
        {
            SUT.RunAsIdentity = Get.User.Unauthorized;
        }

        public async Task When_I_attempt_to_view_an_endpoint_that_requires_authorization()
        {
            _result = await SUT.GetAsync<string>(ApiRoutes.Secured.Get);
        }

        public void Then_I_should_receive_a_not_authorized_warning()
        {
            _result.Model.Should().BeNull();
            _result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
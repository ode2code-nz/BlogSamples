using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Infrastructure.HealthChecks;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Aspects
{
    public class HealthyLivenessCheck : ScenarioFor<AsyncApiDriver, DevOpsStory>
    {
        private ApiResponse<LiveHealthCheck> _result;

        public void Given_the_application_is_running()
        {
        }

        public async Task When_I_check_the_liveness_health()
        {
            _result = await SUT.GetAsync<LiveHealthCheck>("health/live");
        }

        public void Then_the_status_should_be_healthy()
        {
            _result.Model.OverallStatus.Should().Be("Healthy");
        }

        public void AndThen_the_status_code_should_be_Ok()
        {
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
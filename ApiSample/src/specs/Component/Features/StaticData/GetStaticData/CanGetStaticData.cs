using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using ApiSample.Api.Features;
using ApiSample.SharedModels.v1.StaticData;
using Specs.Library.ApiSample.Drivers.Api;

namespace Specs.Component.ApiSample.Features.StaticData.GetStaticData
{

    public abstract class GetStaticData<T> : ScenarioFor<AsyncApiDriver, GetStaticDataStory> where T : StaticDataModel
    {
        private ApiResponse<List<T>> _result;
        protected abstract string Route { get; }

        public void Given_there_is_static_data()
        {
        }

        public async Task When_I_select_the_static_data()
        {
            _result = await SUT.GetAllAsync<T>(Route);
        }

        public void Then_the_data_is_returned()
        {
            _result.Model.Count.Should().BeGreaterThan(0);
            _result.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }

    public class CanGetCompanyStaticData : GetStaticData<CompanyModel>
    {
        protected override string Route => ApiRoutes.StaticData.GetCompanies;
    }

    public class CanGetLocationStaticData : GetStaticData<LocationModel>
    {
        protected override string Route => ApiRoutes.StaticData.GetLocations;
    }
}
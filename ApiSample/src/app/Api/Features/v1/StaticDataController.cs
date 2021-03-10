using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiSample.Domain.Abstractions;
using ApiSample.SharedModels.v1.Responses;
using ApiSample.SharedModels.v1.StaticData;

namespace ApiSample.Api.Features.v1
{
    [Authorize]
    public class StaticDataController : ApiController
    {
        private readonly IStaticDataRepository _repository;

        public StaticDataController(IStaticDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet(ApiRoutes.StaticData.GetCompanies, Name = nameof(ApiRoutes.StaticData.GetCompanies))]
        [ProducesResponseType(typeof(CompanyModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CompanyModel>>> GetCompanies(CancellationToken cancellationToken)
        {
            var data = await _repository.GetStaticDataAsync(cancellationToken);
            var list = data.Companies
                .OrderBy(x => x.Value.Description)
                .Select(x => new CompanyModel { Id = x.Key, Description = x.Value.Description, Name = x.Value.Name });
            return Ok(new SuccessResponse<IEnumerable<CompanyModel>>(list));
        }

        [HttpGet(ApiRoutes.StaticData.GetLocations, Name = nameof(ApiRoutes.StaticData.GetLocations))]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocations(CancellationToken cancellationToken)
        {
            var data = await _repository.GetStaticDataAsync(cancellationToken);
            var list = data.Locations
                .OrderBy(x => x.Value.Description)
                .Select(x => new LocationModel() { Id = x.Key, Description = x.Value.Description, Name = x.Value.Name });
            return Ok(new SuccessResponse<IEnumerable<LocationModel>>(list));
        }
    }
}
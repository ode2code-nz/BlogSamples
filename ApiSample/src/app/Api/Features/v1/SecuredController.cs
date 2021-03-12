using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiSample.SharedModels.v1.Responses;

namespace ApiSample.Api.Features.v1
{
    [Authorize]
    public class SecuredController : ApiController
    {
        [HttpGet(ApiRoutes.Secured.Get)]
        public async Task<IActionResult> GetSecuredData()
        {
            return Ok(new SuccessResponse<string>("This Secured Data is available only for Authenticated Users."));
        }

        [HttpPost(ApiRoutes.Secured.Post)]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PostSecuredData()
        {
            return Ok(new SuccessResponse<string>("This Secured Data is available only for Administrators."));
        }
    }
}
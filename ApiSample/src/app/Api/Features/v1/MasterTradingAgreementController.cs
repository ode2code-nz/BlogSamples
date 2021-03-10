using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiSample.Api.Common.Web;
using ApiSample.Api.Features.v1.MasterTradingAgreements;
using ApiSample.SharedModels.v1.MasterTradingAgreements;
using ApiSample.SharedModels.v1.Responses;

namespace ApiSample.Api.Features.v1
{
    [Authorize()]
    public class MasterTradingAgreementController : ApiController
    {
        // GET: /MasterTradingAgreement
        /// <summary>
        /// Gets all the Master Trading Agreements
        /// </summary>
        [HttpGet(ApiRoutes.MasterTradingAgreement.GetAll)]
        [ProducesResponseType(typeof(SuccessResponse<List<MasterTradingAgreementResponse>>), 200)]
        public async Task<ActionResult<List<MasterTradingAgreementResponse>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllMasterTradingAgreementsQuery());
            return result.ToGetResult<List<MasterTradingAgreementResponse>, List<MasterTradingAgreementDto>>(Mapper);
        }

        // GET: /MasterTradingAgreement/5
        /// <summary>
        /// Gets a single Master Trading Agreement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.MasterTradingAgreement.Get)]
        [ProducesResponseType(typeof(SuccessResponse<MasterTradingAgreementResponse>), 200)]
        public async Task<ActionResult<MasterTradingAgreementResponse>> Get(int id)
        {
            var result = await Mediator.Send(new GetMasterTradingAgreementQuery { MasterTradingAgreementId = id });
            return result.ToGetResult<MasterTradingAgreementResponse, MasterTradingAgreementDto>(Mapper);
        }

        // POST: /MasterTradingAgreement
        /// <summary>
        /// Creates a new Master Trading Agreement
        /// </summary>T
        /// <param name="request"></param>
        [HttpPost(ApiRoutes.MasterTradingAgreement.Create)]
        [ProducesResponseType(typeof(SuccessResponse<RecordsCreatedResponse>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create(CreateMasterTradingAgreementRequest request)
        {
            var result = await Send<CreateMasterTradingAgreementCommand>(request);
            return result.ToCreatedResult("MasterTradingAgreement", nameof(Get));
        }

        // PUT: /MasterTradingAgreement/5
        /// <summary>
        /// Update an existing Master Trading Agreement
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut(ApiRoutes.MasterTradingAgreement.Update)]
        public async Task<IActionResult> Update(UpdateMasterTradingAgreementRequest request)
        {
            var result = await Send<UpdateMasterTradingAgreementCommand>(request);
            return result.ToUpdatedResult();
        }

        // DELETE: /MasterTradingAgreement/5
        /// <summary>
        /// Delete a Master Trading Agreement
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete(ApiRoutes.MasterTradingAgreement.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteMasterTradingAgreementCommand { Id = id });
            return result.ToDeletedResult();
        }
    }
}
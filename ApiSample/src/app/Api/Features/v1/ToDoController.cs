using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiSample.Api.Common.Web;
using ApiSample.Api.Features.v1.ToDoItems;
using ApiSample.SharedModels.v1.Responses;
using ApiSample.SharedModels.v1.ToDoItems;

namespace ApiSample.Api.Features.v1
{
    [Authorize]
    public class ToDoController : ApiController
    {
        // GET: /ToDo
        /// <summary>
        /// Gets all the To Do items
        /// </summary>
        [HttpGet(ApiRoutes.ToDo.GetAll)]
        [ProducesResponseType(typeof(SuccessResponse<List<ToDoItemResponse>>), 200)]
        public async Task<ActionResult<List<ToDoItemResponse>>> GetAll()
        {
            var result = await Mediator.Send(new GetAllToDosQuery());
            return result.ToGetResult<List<ToDoItemResponse>, List<ToDoItemDto>>(Mapper);
        }

        // GET: /ToDo/5
        /// <summary>
        /// Gets a single To Do item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet(ApiRoutes.ToDo.Get)]
        [ProducesResponseType(typeof(SuccessResponse<ToDoItemResponse>), 200)]
        public async Task<ActionResult<ToDoItemResponse>> Get(int id)
        {
            var result = await Mediator.Send(new GetToDoItemQuery {ToDoItemId = id});
            return result.ToGetResult<ToDoItemResponse, ToDoItemDto>(Mapper);
        }

        // POST: /ToDo
        /// <summary>
        /// Creates a new To Do item
        /// </summary>T
        /// <param name="request"></param>
        [HttpPost(ApiRoutes.ToDo.Create)]
        [ProducesResponseType(typeof(SuccessResponse<RecordsCreatedResponse>), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create(CreateToDoItemRequest request)
        {
            var result = await Send<CreateToDoItemCommand>(request);
            return result.ToCreatedResult("ToDo", nameof(Get));
        }

        // PUT: /ToDo/5
        /// <summary>
        /// Update an existing To Do item
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut(ApiRoutes.ToDo.Update)]
        public async Task<IActionResult> Update(UpdateToDoItemRequest request)
        {
            var result = await Send<UpdateToDoItemCommand>(request);
            return result.ToUpdatedResult();
        }

        // DELETE: /ToDo/5
        /// <summary>
        /// Delete a To Do item
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete(ApiRoutes.ToDo.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteToDoItemCommand { Id = id });
            return result.ToDeletedResult();
        }
    }
}
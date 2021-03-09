using System;
using System.Linq;
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Common.FluentResult;
using ToDo.SharedModels.v1.Responses;

namespace ToDo.Api.Common.Web
{
    public static class AspNetResultExtensions
    {
        public static ActionResult<T> ToGetResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(new SuccessResponse<T>(result.Value));
            }
            else
            {
                return new NotFoundResult();
            }
        }

        public static ActionResult<T> ToGetResult<T,U>(this Result<U> result, IMapper mapper)
        {
            if (result.IsSuccess)
            {
                var response = mapper.Map<T>(result.Value);
                return new OkObjectResult(new SuccessResponse<T>(response));
            }
            else
            {
                return new NotFoundResult();
            }
        }
        public static IActionResult ToCreatedResult(this Result result, 
            string controllerName, string actionName = "Get")
        {
            if (result.IsSuccess)
            {
                var reason = result.GetReason<RecordsCreatedSuccess>();
                var response = new SuccessResponse<RecordsCreatedResponse>(
                    ResponseFactory.RecordsCreatedResponse(reason));

                return new CreatedAtActionResult(actionName, controllerName, 
                    new { id = reason.NewIds.First() }, response);
            }
            else
            {
                return result.ToFailureResult();
            }
        }

        public static IActionResult ToUpdatedResult(this Result result)
        {
            return result.IsSuccess ? new NoContentResult() : result.ToFailureResult();
        }

        public static IActionResult ToDeletedResult(this Result result)
        {
            return result.IsSuccess ? new NoContentResult() : result.ToFailureResult();
        }

        public static IActionResult ToFailureResult(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new ArgumentException("Must be failed result");
            }

            var reason = result.GetReasonOrDefault<AppError>();
            if (reason is RecordsNotFoundAppError)
            {
                return new NotFoundResult();
            }

            return new BadRequestObjectResult(ResponseFactory.Error(result));
        }
    }
}
using System;
using System.Linq;
using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Todo.Domain.Common.FluentResult;
using Todo.SharedModels.v1.Responses;

namespace Todo.Api.Common.Web
{
    public static class ResponseFactory
    {
        public static ErrorResponse Error(ResultBase result,
            string id = null,
            short status = (short)HttpStatusCode.BadRequest)
        {
            var response = new ErrorResponse
            {
                ErrorId = id ?? Guid.NewGuid().ToString(),
                StatusCode = status,
                Title = status == (short)HttpStatusCode.BadRequest
                    ? "One or more validation failures have occurred."
                    : "Some kind of error occurred in the API.  Please use the id and contact our " +
                      "support team if the problem persists."
            };

            foreach (var error in result.GetFailures())
            {
                response.Errors.Add(new ErrorModel(error.PropertyName, error.Message, error.RowKey));
            }

            return response;
        }

        public static ErrorResponse Error(Exception exception, 
            string id = null, 
            short status = (short)HttpStatusCode.InternalServerError)
        {
            var response = new ErrorResponse
            {
                ErrorId = id ?? Guid.NewGuid().ToString(),
                StatusCode = status,
                Title = "Some kind of error occurred in the API.  Please use the id and contact our " +
                          "support team if the problem persists."
            };
            response.Errors.Add(new ErrorModel(null, exception.Message));
            return response;
        }

        public static ErrorResponse Error(ModelStateDictionary modelState,
            string id = null,
            short status = (short)HttpStatusCode.BadRequest)
        {
            return new ErrorResponse
            {
                ErrorId = id ?? Guid.NewGuid().ToString(),
                StatusCode = status,
                Title = "One or more validation failures have occurred.",
                Errors = modelState.Keys.SelectMany<string, ErrorModel>(key => modelState[key].Errors
                        .Select(x => new ErrorModel(key, x.ErrorMessage)))
                    .ToList()
            };
        }

        public static RecordsCreatedResponse RecordsCreatedResponse(RecordsCreatedSuccess reason)
        {
            return new RecordsCreatedResponse(reason.NewIds){Message = reason.Message};
        }
    }
}

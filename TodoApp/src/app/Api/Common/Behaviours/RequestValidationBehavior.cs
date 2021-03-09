using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Todo.Api.Common.Validation;
using Todo.Api.Common.Web;

namespace Todo.Api.Common.Behaviours
{
    public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : CommandBase
        where TResponse : ResultBase
    {
        private readonly ValidationService _validationService;
        private readonly ILogger<TRequest> _logger;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validationService = new ValidationService(validators);
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationResult = _validationService.ValidateCommand(command);

            if (validationResult.IsFailed)
            {
                return validationResult as TResponse;
            }

            var handlerResult = await next();

            var result = _validationService.EvaluateResults(command.IgnoreWarnings, validationResult, handlerResult);

            if (result.IsFailed)
            {
                var requestName = typeof(TRequest).Name;
                var error = ResponseFactory.Error(result);
                _logger.LogDebug("Request validation failure: {Name} {ErrorMessage}", requestName, error);
            }

            return result as TResponse;
        }
    }
}
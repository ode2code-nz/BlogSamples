using System.Collections.Generic;
using System.Linq;
using FluentResults;
using FluentValidation;
using ApiSample.Domain.Common.FluentResult;
using Serilog;

namespace ApiSample.Api.Common.Validation
{
    public class ValidationService
    {
        public IEnumerable<IValidator> FluentValidators { get; }

        public ValidationService(IEnumerable<IValidator> fluentValidators)
        {
            FluentValidators = fluentValidators;
        }

        public Result ValidateCommand<TCommand>(TCommand command) where TCommand : CommandBase
        {
            var result = Result.Ok();

            if (!FluentValidators.Any())
            {
                return result;
            }

            var context = new ValidationContext<TCommand>(command);

            var failures = FluentValidators
                .Select(v => v.Validate(context))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count == 0)
            {
                return result;
            }

            Log.Warning("One or more validation failures have occurred.: {Name} {@ValidationErrors} {@Request}",
                command.GetType().Name, failures, command);

            if (failures.Any(x => x.Severity == Severity.Error))
            {
                return Result.Fail("Validation failures")
                    .WithValidationResults(failures);
            }

            if (failures.Any(x => x.Severity == Severity.Warning))
            {
                if (command.IgnoreWarnings)
                {
                    return Result.Ok()
                        .WithValidationResults(failures);
                }
                else
                {
                    return Result.Fail("Validation failures")
                        .WithValidationResults(failures);
                }
            }

            return result;
        }

        public Result EvaluateResults(bool ignoreWarnings, params ResultBase[] results)
        {
            var result = Result.Merge(results);

            if (result.HasErrors())
            {
                return result;
            }

            if (result.HasWarnings() && ignoreWarnings == false)
            {
                return result.WithReason(new TreatWarningsAsErrors());
            }

            return result;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using FluentResults;

namespace Todo.Domain.Common.FluentResult
{
    public static class ResultExtensions
    {
        public static TReason GetReason<TReason>(this ResultBase result) where TReason : Reason
        {
            return result.Reasons.OfType<TReason>().Single();
        }

        public static TReason GetReasonOrDefault<TReason>(this ResultBase result) where TReason : Reason
        {
            return result.Reasons.OfType<TReason>().FirstOrDefault();
        }

        public static Result AddResult(this Result result, ResultBase newResult)
        {
            result.Reasons.AddRange(newResult.Reasons);
            return result;
        }

        public static ResultBase AddRecordsNotFound(this ResultBase result, string propertyName, int id)
        {
            result.Reasons.Add(new RecordsNotFoundAppError(propertyName, id));
            return result;
        }

        public static ResultBase AddError(this ResultBase result, string propertyName, string message, int rowKey = int.MinValue)
        {
            result.Reasons.Add(new AppError(propertyName, message, rowKey));
            return result;
        }

        public static ResultBase AddError(this ResultBase result, string message, int rowKey = int.MinValue)
        {
            result.Reasons.Add(new AppError(message, rowKey));
            return result;
        }

        public static ResultBase AddWarning(this ResultBase result, string propertyName, string message, int rowKey = int.MinValue)
        {
            result.Reasons.Add(new AppWarning(propertyName, message, rowKey));
            return result;
        }

        public static ResultBase AddWarning(this ResultBase result, string message, int rowKey = int.MinValue)
        {
            result.Reasons.Add(new AppWarning(message, rowKey));
            return result;
        }

        public static IEnumerable<IFailure> GetFailures(this ResultBase result)
        {
            foreach (var error in result.GetErrors())
            {
                yield return error;
            }

            foreach (var warning in result.GetWarnings())
            {
                yield return warning;
            }
        }

        public static List<AppError> GetErrors(this ResultBase result)
        {
            return result.Reasons
                .OfType<AppError>()
                .ToList();
        }

        public static List<AppWarning> GetWarnings(this ResultBase result)
        {
            return result.Reasons
                .OfType<AppWarning>()
                .ToList();
        }

        public static bool HasFailures(this ResultBase result)
        {
            return result.HasErrors() || result.HasWarnings();
        }

        public static bool HasErrors(this ResultBase result)
        {
            return result.GetErrors().Any();
        }

        public static bool HasWarnings(this ResultBase result)
        {
            return result.GetWarnings().Any();
        }
    }
}
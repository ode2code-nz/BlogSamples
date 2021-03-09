using FluentAssertions;
using FluentResults;
using ToDo.Domain.Common.FluentResult;

namespace Specs.Unit.ToDo.Domain.Common.FluentResult
{
    public class WhenCreatingResults : ScenarioFor<Result>
    {
        private static string _propertyName = "property";
        private static string _message = "message";
        private static int _id = 99;

        public void Then_RecordNotFound_should_have_RecordsNotFoundAppError_reason()
        {
            SUT = ResultFactory.RecordNotFound(_propertyName, _id);
            SUT.ShouldHaveSingleFailureReason<RecordsNotFoundAppError>(_propertyName, $"Record '{_id}' not found", _id);
        }

        public void AndThen_Error_should_have_AppError_reason()
        {
            SUT = ResultFactory.Error(_message);
            SUT.ShouldHaveSingleFailureReason<AppError>(null, _message);

            SUT = ResultFactory.Error(_propertyName, _message);
            SUT.ShouldHaveSingleFailureReason<AppError>(_propertyName, _message);

            SUT = ResultFactory.Error(_propertyName, _message, _id);
            SUT.ShouldHaveSingleFailureReason<AppError>(_propertyName, _message, _id);
        }

        public void AndThen_Warning_should_have_AppWarning_reason()
        {
            SUT = ResultFactory.Warning(_message);
            SUT.ShouldHaveSingleFailureReason<AppWarning>(null, _message);

            SUT = ResultFactory.Warning(_propertyName, _message);
            SUT.ShouldHaveSingleFailureReason<AppWarning>(_propertyName, _message);

            SUT = ResultFactory.Warning(_propertyName, _message, _id);
            SUT.ShouldHaveSingleFailureReason<AppWarning>(_propertyName, _message, _id);
        }

        public void AndThen_CreateResultForFailure_should_create_error()
        {
            SUT = ResultFactory.CreateResultForFailure(ValidationSeverity.Error, _propertyName, _message);
            SUT.ShouldHaveSingleFailureReason<AppError>(_propertyName, _message);

            SUT = ResultFactory.CreateResultForFailure(ValidationSeverity.Error, _propertyName, _message, _id);
            SUT.ShouldHaveSingleFailureReason<AppError>(_propertyName, _message, _id);
        }

        public void AndThen_CreateResultForFailure_should_create_warning()
        {
            SUT = ResultFactory.CreateResultForFailure(ValidationSeverity.Warning, _propertyName, _message);
            SUT.ShouldHaveSingleFailureReason<AppWarning>(_propertyName, _message);

            SUT = ResultFactory.CreateResultForFailure(ValidationSeverity.Warning, _propertyName, _message, _id);
            SUT.ShouldHaveSingleFailureReason<AppWarning>(_propertyName, _message, _id);
        }

        public void AndThen_CreateReasonForFailure_should_create_error()
        {
            ResultFactory.CreateReasonForFailure(ValidationSeverity.Error, _propertyName, _message)
                .As<AppError>()
                .ShouldBeFailureReason<AppError>(_propertyName, _message);

            ResultFactory.CreateReasonForFailure(ValidationSeverity.Error, _propertyName, _message, _id)
                .As<AppError>()
                .ShouldBeFailureReason<AppError>(_propertyName, _message, _id);
        }

        public void AndThen_CreateReasonForFailure_should_create_warning()
        {
            ResultFactory.CreateReasonForFailure(ValidationSeverity.Warning, _propertyName, _message)
                .As<AppWarning>()
                .ShouldBeFailureReason<AppWarning>(_propertyName, _message);

            ResultFactory.CreateReasonForFailure(ValidationSeverity.Warning, _propertyName, _message, _id)
                .As<AppWarning>()
                .ShouldBeFailureReason<AppWarning>(_propertyName, _message, _id);
        }
    }

    public static class ResultAssertions
    {
        public static void ShouldHaveSingleFailureReason<T>(this Result result, string propertyName, string message = null, int rowKey = int.MinValue) 
            where T : Reason, IFailure
        {
            result.Reasons.Count.Should().Be(1);
            var reason = result.GetReasonOrDefault<T>();
            reason.ShouldBeFailureReason(propertyName, message, rowKey);
        }

        public static void ShouldBeFailureReason<T>(this T reason, string propertyName, string message = null, int rowKey = int.MinValue)
            where T : Reason, IFailure
        {
            reason.Should().NotBeNull();
            reason.PropertyName.Should().Be(propertyName);
            reason.Message.Should().Be(message);
            reason.RowKey.Should().Be(rowKey);
        }
    }
}

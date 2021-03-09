using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using ToDo.SharedModels.v1.Responses;
using Specs.Library.ToDo.Drivers.Api;

namespace Specs.Library.ToDo.Extensions
{
    public static class ApiResponseAssertions
    {
        public static void ShouldContainErrors(this ApiResponse response, params string[] errors)
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            response.Errors.Should().NotBeEmpty();
            response.Success.Should().BeFalse();

            foreach (var error in errors)
            {
                if (response.Errors.Count(x => x.Message == error) == 0)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"Did not receive expected error {error}");
                    sb.AppendLine();
                    sb.AppendLine("Did receive errors:");
                    sb.AppendLine(response.ErrorsAsString());
                    throw new ApiDriverException(sb.ToString());
                }
            }
        }

        public static void ShouldHaveError(this ApiResponse response, string propertyName, string errorMessage, int count = 1)
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            response.Errors.Should().NotBeEmpty();
            response.Success.Should().BeFalse();

            var errors = response.ErrorsForProperty(propertyName);
            if (errors.Count == 0)
            {
                throw new ApplicationException($"There were no errors for property '{propertyName}'");
            }

            if(errors.Count(x => x.PropertyName == propertyName && x.Message == errorMessage) == 0)
            {
                var message = $"Expected error for property '{propertyName}' was {errorMessage}. Actual error was '{errors.First().Message}'";
                throw new ApplicationException(message);
            }
        }

        public static void ShouldHaveErrorWithRowKey(this ApiResponse response, string propertyName, string errorMessage, int rowKey, int count = 1)
        {
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            response.Errors.Should().NotBeEmpty();
            response.Success.Should().BeFalse();

            var errors = response.ErrorsForProperty(propertyName);
            if (errors.Count == 0)
            {
                throw new ApplicationException($"There were no errors for property '{propertyName}'");
            }

            if (errors.Count(x => x.PropertyName == propertyName && x.Message == errorMessage && x.RowKey == rowKey) == 0)
            {
                var message = $"Expected error for property '{propertyName}' was {errorMessage} with row key '{rowKey}'. Actual error was '{errors.First().Message}' with row key '{errors.First().RowKey}'";
                throw new ApplicationException(message);
            }
        }

        public static void ShouldNotContainErrors(this HttpResponseMessage message)
        {
            throw new NotImplementedException();

            //var errorResponse = JsonConvert.DeserializeObject<ValidationProblemDetails>(message.Content.ReadAsStringAsync().Result);

            //errorResponse?.Errors?.Count.Should().Be(0);
        }

        public static void ShouldBeCreatedResult(this ApiResponse<RecordsCreatedResponse> response, string location)
        {
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Model.NewIds.First().Should().BeGreaterThan(0);
            response.Headers.Location.AbsolutePath.Should().Be(location);
        }
    }
}
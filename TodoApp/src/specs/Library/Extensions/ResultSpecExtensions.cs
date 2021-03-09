using FluentAssertions;
using FluentResults;

namespace Specs.Library.Todo.Extensions
{
    public static class ResultSpecExtensions
    {
        public static void ShouldHaveError(this ResultBase result, string errorMessage)
        {
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().Be(errorMessage);
        }
    }
}
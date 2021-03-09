using FluentResults;
using MediatR;

namespace Todo.Api.Common.Validation
{
    public class CommandBase : IRequest<Result>
    {
        public bool IgnoreWarnings { get; set; } = false;
    }
}
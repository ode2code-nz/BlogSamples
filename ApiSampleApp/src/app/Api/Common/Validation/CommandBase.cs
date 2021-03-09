using FluentResults;
using MediatR;

namespace ToDo.Api.Common.Validation
{
    public class CommandBase : IRequest<Result>
    {
        public bool IgnoreWarnings { get; set; } = false;
    }
}
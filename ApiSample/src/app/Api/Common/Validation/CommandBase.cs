using FluentResults;
using MediatR;

namespace ApiSample.Api.Common.Validation
{
    public class CommandBase : IRequest<Result>
    {
        public bool IgnoreWarnings { get; set; } = false;
    }
}
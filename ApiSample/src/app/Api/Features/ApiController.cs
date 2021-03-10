using System.Threading.Tasks;
using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSample.Api.Features
{
    [ApiController]
  //  [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        private IMapper _mapper;
        protected IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();

        public async Task<Result> Send<TCommand>(object request)
            where TCommand : IRequest<Result>
        {
            var command = Mapper.Map<TCommand>(request);
            var result = await Mediator.Send<Result>(command);
            return result;
        }
    }
}

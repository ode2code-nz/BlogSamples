using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ApiSample.Domain.Common.FluentResult;
using ApiSample.Infrastructure.Interfaces;

namespace ApiSample.Api.Features.v1.ToDoItems
{
    public class GetToDoItemQuery : IRequest<Result<ToDoItemDto>>
    {
        public int ToDoItemId { get; set; }
    }

    public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, Result<ToDoItemDto>>
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public GetToDoItemQueryHandler(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result<ToDoItemDto>> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
        {
            var item = await _db.ToDoItems
                .Where(x => x.Id == request.ToDoItemId)
                .ProjectTo<ToDoItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return item == null 
                ? ResultFactory.RecordNotFound(nameof(request.ToDoItemId), request.ToDoItemId).ToResult<ToDoItemDto>() 
                : Result.Ok<ToDoItemDto>(item);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Model.ToDos;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Api.Features.v1.ToDoItems
{
    public class GetAllToDosQuery : IRequest<Result<List<ToDoItemDto>>>
    {
    }

    public class GetAllToDosQueryHandler : IRequestHandler<GetAllToDosQuery, Result<List<ToDoItemDto>>>
    {
        private readonly IQueryDb _db;
        private readonly IMapper _mapper;

        public GetAllToDosQueryHandler(IQueryDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result<List<ToDoItemDto>>> Handle(GetAllToDosQuery request, CancellationToken cancellationToken)
        {
            var items = await _db.QueryFor<ToDoItem>()
                .ProjectTo<ToDoItemDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Title)
                .ToListAsync(cancellationToken);

            return Result.Ok<List<ToDoItemDto>>(items);
        }
    }
}

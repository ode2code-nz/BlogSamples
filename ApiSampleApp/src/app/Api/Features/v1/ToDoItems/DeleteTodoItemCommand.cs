using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ApiSample.Domain.Common.FluentResult;
using ApiSample.Infrastructure.Interfaces;

namespace ApiSample.Api.Features.v1.ToDoItems
{
    public class DeleteToDoItemCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public DeleteToDoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ToDoItems.FindAsync(request.Id);

            if (entity == null)
            {
                return ResultFactory.RecordNotFound("Id", request.Id);
            }

            _context.ToDoItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using MediatR;
using Todo.Api.Common.Mappings;
using Todo.Api.Common.Validation;
using Todo.Domain.Common.FluentResult;
using Todo.Domain.Model.ToDos;
using Todo.Infrastructure.Interfaces;
using Todo.SharedModels.v1.ToDoItems;

namespace Todo.Api.Features.v1.ToDoItems
{
    public class UpdateTodoItemCommand : CommandBase, IMapFrom<UpdateTodoItemRequest>
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool IsDone { get; set; }
    }

    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(x => x.Email)
                .Must(y => Email.Create(y).IsSuccess == true)
                .WithMessage("Must be valid email");

            RuleFor(x => x.Email).MustBeValidEmail();
        }
    }

    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public UpdateTodoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ToDoItems.FindAsync(request.Id);

            if (entity == null)
            {
                return ResultFactory.RecordNotFound("Id", request.Id);
            }

            entity.Update(request.Title, request.Description, Email.Create(request.Email).Value, request.IsDone);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

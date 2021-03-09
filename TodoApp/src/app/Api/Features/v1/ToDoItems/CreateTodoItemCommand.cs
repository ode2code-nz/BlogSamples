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
    public class CreateTodoItemCommand : CommandBase, IMapFrom<CreateTodoItemRequest>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }

    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(200)
                .NotEmpty();
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public CreateTodoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new ToDoItem(request.Title, request.Description, Email.Create(request.Email).Value);

            _context.ToDoItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok()
                .WithReason(new RecordsCreatedSuccess(entity.Id));
        }
    }
}
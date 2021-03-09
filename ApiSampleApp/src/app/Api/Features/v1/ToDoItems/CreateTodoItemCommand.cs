using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using MediatR;
using ToDo.Api.Common.Mappings;
using ToDo.Api.Common.Validation;
using ToDo.Domain.Common.FluentResult;
using ToDo.Domain.Model.ToDos;
using ToDo.Infrastructure.Interfaces;
using ToDo.SharedModels.v1.ToDoItems;

namespace ToDo.Api.Features.v1.ToDoItems
{
    public class CreateToDoItemCommand : CommandBase, IMapFrom<CreateToDoItemRequest>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }

    public class CreateToDoItemCommandValidator : AbstractValidator<CreateToDoItemCommand>
    {
        public CreateToDoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(200)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(200)
                .NotEmpty();
        }
    }

    public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public CreateToDoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new ToDoItem(request.Title, request.Description, Email.Create(request.Email).Value);

            _context.ToDoItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok()
                .WithReason(new RecordsCreatedSuccess(entity.Id));
        }
    }
}
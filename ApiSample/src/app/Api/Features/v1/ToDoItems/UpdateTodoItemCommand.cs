using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using MediatR;
using ApiSample.Api.Common.Mappings;
using ApiSample.Api.Common.Validation;
using ApiSample.Domain.Common.FluentResult;
using ApiSample.Domain.Model.ToDos;
using ApiSample.Infrastructure.Interfaces;
using ApiSample.SharedModels.v1.ToDoItems;

namespace ApiSample.Api.Features.v1.ToDoItems
{
    public class UpdateToDoItemCommand : CommandBase, IMapFrom<UpdateToDoItemRequest>
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public bool IsDone { get; set; }
    }

    public class UpdateToDoItemCommandValidator : AbstractValidator<UpdateToDoItemCommand>
    {
        public UpdateToDoItemCommandValidator()
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

    public class UpdateToDoItemCommandHandler : IRequestHandler<UpdateToDoItemCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public UpdateToDoItemCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
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

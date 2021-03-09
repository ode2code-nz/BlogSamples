using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using MediatR;
using ToDo.Domain.Common.FluentResult;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Api.Features.v1.MasterTradingAgreements
{
    public class DeleteMasterTradingAgreementCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class DeleteMasterTradingAgreementCommandHandler : IRequestHandler<DeleteMasterTradingAgreementCommand, Result>
    {
        private readonly IUnitOfWork _context;

        public DeleteMasterTradingAgreementCommandHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteMasterTradingAgreementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MasterTradingAgreements.FindAsync(request.Id);

            if (entity == null)
            {
                return ResultFactory.RecordNotFound("Id", request.Id);
            }

            _context.MasterTradingAgreements.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
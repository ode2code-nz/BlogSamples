using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Common.FluentResult;
using Todo.Infrastructure.Interfaces;

namespace Todo.Api.Features.v1.MasterTradingAgreements
{
    public class GetMasterTradingAgreementQuery : IRequest<Result<MasterTradingAgreementDto>>
    {
        public int MasterTradingAgreementId { get; set; }
    }

    public class GetMasterTradingAgreementQueryHandler : IRequestHandler<GetMasterTradingAgreementQuery, Result<MasterTradingAgreementDto>>
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public GetMasterTradingAgreementQueryHandler(IUnitOfWork db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result<MasterTradingAgreementDto>> Handle(GetMasterTradingAgreementQuery request, CancellationToken cancellationToken)
        {
            var item = await _db.MasterTradingAgreements
                .Where(x => x.Id == request.MasterTradingAgreementId)
                .ProjectTo<MasterTradingAgreementDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return item == null 
                ? ResultFactory.RecordNotFound(nameof(request.MasterTradingAgreementId), request.MasterTradingAgreementId).ToResult<MasterTradingAgreementDto>() 
                : Result.Ok<MasterTradingAgreementDto>(item);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Model.MasterTradingAgreements;
using ToDo.Infrastructure.Interfaces;

namespace ToDo.Api.Features.v1.MasterTradingAgreements
{
    public class GetAllMasterTradingAgreementsQuery : IRequest<Result<List<MasterTradingAgreementDto>>>
    {
    }

    public class GetAllMasterTradingAgreementsQueryHandler : IRequestHandler<GetAllMasterTradingAgreementsQuery, Result<List<MasterTradingAgreementDto>>>
    {
        private readonly IQueryDb _db;
        private readonly IMapper _mapper;

        public GetAllMasterTradingAgreementsQueryHandler(IQueryDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Result<List<MasterTradingAgreementDto>>> Handle(GetAllMasterTradingAgreementsQuery request, CancellationToken cancellationToken)
        {
            var items = await _db.QueryFor<MasterTradingAgreement>().Include(x => x.ContractSchedules)
                .ProjectTo<MasterTradingAgreementDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Name)
                .ToListAsync(cancellationToken);

            return Result.Ok(items);
        }
    }
}

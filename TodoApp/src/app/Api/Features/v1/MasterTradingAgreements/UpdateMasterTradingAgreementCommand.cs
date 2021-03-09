using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDo.Api.Common.Mappings;
using ToDo.Api.Common.Validation;
using ToDo.Domain.Abstractions;
using ToDo.Domain.Common.FluentResult;
using ToDo.Domain.Model;
using ToDo.Domain.Model.MasterTradingAgreements;
using ToDo.Domain.Model.StaticData;
using ToDo.Infrastructure.Data;
using ToDo.SharedModels.v1.MasterTradingAgreements;

namespace ToDo.Api.Features.v1.MasterTradingAgreements
{
    public class UpdateMasterTradingAgreementCommand : CommandBase, IMapFrom<UpdateMasterTradingAgreementRequest>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }

        public List<ContractScheduleDto> ContractSchedules { get; set; }

        public DateTime DurationStartDate { get; set; }

        public DateTime DurationEndDate { get; set; }

        public string Comments { get; set; }

        public int CounterpartyId { get; set; }
    }

    public class UpdateMasterTradingAgreementCommandValidator : AbstractValidator<UpdateMasterTradingAgreementCommand>
    {
        public UpdateMasterTradingAgreementCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }

    public class UpdateMasterTradingAgreementCommandHandler : IRequestHandler<UpdateMasterTradingAgreementCommand, Result>
    {
        private readonly AppDbContext _db;
        private readonly StaticDataStore _staticData;

        public UpdateMasterTradingAgreementCommandHandler(AppDbContext db, IStaticDataRepository repository)
        {
            _db = db;
            _staticData = repository.GetStaticDataAsync().Result;
        }

        public async Task<Result> Handle(UpdateMasterTradingAgreementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _db.MasterTradingAgreements
                .Include(x => x.ContractSchedules)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                return ResultFactory.RecordNotFound("Id", request.Id);
            }

            var counterparty = await _db.Companies.FindAsync(request.CounterpartyId);
            if (counterparty is null)
            {
                return ResultFactory.RecordNotFound("Company", request.CounterpartyId);
            }

            var type = (MasterTradingAgreementType)request.Type;
            var duration = new DateRange(request.DurationStartDate, request.DurationEndDate);
            entity.Update(request.Name, type, duration, counterparty, request.Comments);
            foreach (var dto in request.ContractSchedules)
            {
                var location = _staticData.Locations[dto.LocationId];
                if (location is null)
                {
                    return ResultFactory.RecordNotFound("Location", dto.LocationId);
                }

                var scheduleType = (ContractScheduleType)request.Type;
                var scheduleDuration = new DateRange(request.DurationStartDate, request.DurationEndDate);

                var schedule = entity.ContractSchedules.SingleOrDefault(x => x.Id == dto.Id);
                if (schedule is null)
                {
                    return ResultFactory.RecordNotFound("ContractSchedule", dto.Id);
                }

                schedule.Update(dto.Name, scheduleType, scheduleDuration, dto.Comments, entity, location);
            }

            await _db.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}

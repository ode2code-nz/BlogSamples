using System;
using System.Collections.Generic;
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
    public class CreateMasterTradingAgreementCommand : CommandBase, IMapFrom<CreateMasterTradingAgreementRequest>
    {
        public string Name { get; set; }
        public int Type { get; set; }

        public List<ContractScheduleDto> ContractSchedules { get; set; }

        public DateTime DurationStartDate { get; set; }

        public DateTime DurationEndDate { get; set; }

        public string Comments { get; set; }

        public int CounterpartyId { get; set; }
    }

    public class CreateMasterTradingAgreementCommandValidator : AbstractValidator<CreateMasterTradingAgreementCommand>
    {
        public CreateMasterTradingAgreementCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }

    public class CreateMasterTradingAgreementCommandHandler : IRequestHandler<CreateMasterTradingAgreementCommand, Result>
    {
        private readonly AppDbContext _db;
        private readonly StaticDataStore _staticData;

        public CreateMasterTradingAgreementCommandHandler(AppDbContext db, IStaticDataRepository repository)
        {
            _db = db;
            _staticData = repository.GetStaticDataAsync().Result;
        }

        public async Task<Result> Handle(CreateMasterTradingAgreementCommand request, CancellationToken cancellationToken)
        {
            var counterparty = _staticData.Companies[request.CounterpartyId]; 
            if (counterparty is null)
            {
                return ResultFactory.RecordNotFound("Company", request.CounterpartyId);
            }

            _db.Attach(counterparty).State = EntityState.Unchanged;

            var type = (MasterTradingAgreementType) request.Type;
            var duration = new DateRange(request.DurationStartDate, request.DurationEndDate);
            var entity = new MasterTradingAgreement(request.Name, type, duration, counterparty, request.Comments);

            foreach (var dto in request.ContractSchedules)
            {
                var location = _staticData.Locations[dto.LocationId];
                if (location is null)
                {
                    return ResultFactory.RecordNotFound("Location", dto.LocationId);
                }
                _db.Attach(location).State = EntityState.Unchanged;
                
                var scheduleType = (ContractScheduleType)request.Type;
                var scheduleDuration = new DateRange(request.DurationStartDate, request.DurationEndDate);
                var schedule = new ContractSchedule(dto.Name, scheduleType, scheduleDuration,
                    dto.Comments, entity, location);
                entity.AddContractSchedule(schedule);
            }

            _db.MasterTradingAgreements.Add(entity);

            await _db.SaveChangesAsync(cancellationToken);

            return Result.Ok()
                .WithReason(new RecordsCreatedSuccess(entity.Id));
        }
    }
}
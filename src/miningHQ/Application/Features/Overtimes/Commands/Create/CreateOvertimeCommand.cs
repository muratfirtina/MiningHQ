using Application.Features.Overtimes.Constants;
using Application.Features.Overtimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Overtimes.Constants.OvertimesOperationClaims;

namespace Application.Features.Overtimes.Commands.Create;

public class CreateOvertimeCommand : IRequest<CreatedOvertimeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid EmployeeId { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }

    public string[] Roles => new[] { Admin, Write, OvertimesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetOvertimes";

    public class CreateOvertimeCommandHandler : IRequestHandler<CreateOvertimeCommand, CreatedOvertimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly OvertimeBusinessRules _overtimeBusinessRules;

        public CreateOvertimeCommandHandler(IMapper mapper, IOvertimeRepository overtimeRepository,
                                         OvertimeBusinessRules overtimeBusinessRules)
        {
            _mapper = mapper;
            _overtimeRepository = overtimeRepository;
            _overtimeBusinessRules = overtimeBusinessRules;
        }

        public async Task<CreatedOvertimeResponse> Handle(CreateOvertimeCommand request, CancellationToken cancellationToken)
        {
            Overtime overtime = _mapper.Map<Overtime>(request);

            await _overtimeRepository.AddAsync(overtime);

            CreatedOvertimeResponse response = _mapper.Map<CreatedOvertimeResponse>(overtime);
            return response;
        }
    }
}
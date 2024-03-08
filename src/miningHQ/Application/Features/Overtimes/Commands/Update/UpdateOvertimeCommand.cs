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

namespace Application.Features.Overtimes.Commands.Update;

public class UpdateOvertimeCommand : IRequest<UpdatedOvertimeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }

    public string[] Roles => new[] { Admin, Write, OvertimesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetOvertimes";

    public class UpdateOvertimeCommandHandler : IRequestHandler<UpdateOvertimeCommand, UpdatedOvertimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly OvertimeBusinessRules _overtimeBusinessRules;

        public UpdateOvertimeCommandHandler(IMapper mapper, IOvertimeRepository overtimeRepository,
                                         OvertimeBusinessRules overtimeBusinessRules)
        {
            _mapper = mapper;
            _overtimeRepository = overtimeRepository;
            _overtimeBusinessRules = overtimeBusinessRules;
        }

        public async Task<UpdatedOvertimeResponse> Handle(UpdateOvertimeCommand request, CancellationToken cancellationToken)
        {
            Overtime? overtime = await _overtimeRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _overtimeBusinessRules.OvertimeShouldExistWhenSelected(overtime);
            overtime = _mapper.Map(request, overtime);

            await _overtimeRepository.UpdateAsync(overtime!);

            UpdatedOvertimeResponse response = _mapper.Map<UpdatedOvertimeResponse>(overtime);
            return response;
        }
    }
}
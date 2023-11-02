using Application.Features.LeaveUsages.Constants;
using Application.Features.LeaveUsages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LeaveUsages.Constants.LeaveUsagesOperationClaims;

namespace Application.Features.LeaveUsages.Commands.Create;

public class CreateLeaveUsageCommand : IRequest<CreatedLeaveUsageResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; }
    public EmployeeLeave EmployeeLeave { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public string[] Roles => new[] { Admin, Write, LeaveUsagesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetLeaveUsages"};

    public class CreateLeaveUsageCommandHandler : IRequestHandler<CreateLeaveUsageCommand, CreatedLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveUsageRepository _leaveUsageRepository;
        private readonly LeaveUsageBusinessRules _leaveUsageBusinessRules;

        public CreateLeaveUsageCommandHandler(IMapper mapper, ILeaveUsageRepository leaveUsageRepository,
                                         LeaveUsageBusinessRules leaveUsageBusinessRules)
        {
            _mapper = mapper;
            _leaveUsageRepository = leaveUsageRepository;
            _leaveUsageBusinessRules = leaveUsageBusinessRules;
        }

        public async Task<CreatedLeaveUsageResponse> Handle(CreateLeaveUsageCommand request, CancellationToken cancellationToken)
        {
            LeaveUsage leaveUsage = _mapper.Map<LeaveUsage>(request);

            await _leaveUsageRepository.AddAsync(leaveUsage);

            CreatedLeaveUsageResponse response = _mapper.Map<CreatedLeaveUsageResponse>(leaveUsage);
            return response;
        }
    }
}
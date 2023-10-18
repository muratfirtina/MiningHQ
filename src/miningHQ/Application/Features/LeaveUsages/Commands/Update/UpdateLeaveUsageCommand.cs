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

namespace Application.Features.LeaveUsages.Commands.Update;

public class UpdateLeaveUsageCommand : IRequest<UpdatedLeaveUsageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; }
    public EmployeeLeave EmployeeLeave { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public string[] Roles => new[] { Admin, Write, LeaveUsagesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLeaveUsages";

    public class UpdateLeaveUsageCommandHandler : IRequestHandler<UpdateLeaveUsageCommand, UpdatedLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveUsageRepository _leaveUsageRepository;
        private readonly LeaveUsageBusinessRules _leaveUsageBusinessRules;

        public UpdateLeaveUsageCommandHandler(IMapper mapper, ILeaveUsageRepository leaveUsageRepository,
                                         LeaveUsageBusinessRules leaveUsageBusinessRules)
        {
            _mapper = mapper;
            _leaveUsageRepository = leaveUsageRepository;
            _leaveUsageBusinessRules = leaveUsageBusinessRules;
        }

        public async Task<UpdatedLeaveUsageResponse> Handle(UpdateLeaveUsageCommand request, CancellationToken cancellationToken)
        {
            LeaveUsage? leaveUsage = await _leaveUsageRepository.GetAsync(predicate: lu => lu.Id == request.Id, cancellationToken: cancellationToken);
            await _leaveUsageBusinessRules.LeaveUsageShouldExistWhenSelected(leaveUsage);
            leaveUsage = _mapper.Map(request, leaveUsage);

            await _leaveUsageRepository.UpdateAsync(leaveUsage!);

            UpdatedLeaveUsageResponse response = _mapper.Map<UpdatedLeaveUsageResponse>(leaveUsage);
            return response;
        }
    }
}
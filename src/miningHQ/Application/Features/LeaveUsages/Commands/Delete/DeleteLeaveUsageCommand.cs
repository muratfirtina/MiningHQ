using Application.Features.LeaveUsages.Constants;
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

namespace Application.Features.LeaveUsages.Commands.Delete;

public class DeleteLeaveUsageCommand : IRequest<DeletedLeaveUsageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LeaveUsagesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLeaveUsages";

    public class DeleteLeaveUsageCommandHandler : IRequestHandler<DeleteLeaveUsageCommand, DeletedLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveUsageRepository _leaveUsageRepository;
        private readonly LeaveUsageBusinessRules _leaveUsageBusinessRules;

        public DeleteLeaveUsageCommandHandler(IMapper mapper, ILeaveUsageRepository leaveUsageRepository,
                                         LeaveUsageBusinessRules leaveUsageBusinessRules)
        {
            _mapper = mapper;
            _leaveUsageRepository = leaveUsageRepository;
            _leaveUsageBusinessRules = leaveUsageBusinessRules;
        }

        public async Task<DeletedLeaveUsageResponse> Handle(DeleteLeaveUsageCommand request, CancellationToken cancellationToken)
        {
            LeaveUsage? leaveUsage = await _leaveUsageRepository.GetAsync(predicate: lu => lu.Id == request.Id, cancellationToken: cancellationToken);
            await _leaveUsageBusinessRules.LeaveUsageShouldExistWhenSelected(leaveUsage);

            await _leaveUsageRepository.DeleteAsync(leaveUsage!);

            DeletedLeaveUsageResponse response = _mapper.Map<DeletedLeaveUsageResponse>(leaveUsage);
            return response;
        }
    }
}
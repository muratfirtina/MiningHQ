using Application.Features.LeaveUsages.Constants;
using Application.Features.LeaveUsages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LeaveUsages.Constants.LeaveUsagesOperationClaims;

namespace Application.Features.LeaveUsages.Queries.GetById;

public class GetByIdLeaveUsageQuery : IRequest<GetByIdLeaveUsageResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLeaveUsageQueryHandler : IRequestHandler<GetByIdLeaveUsageQuery, GetByIdLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveUsageRepository _leaveUsageRepository;
        private readonly LeaveUsageBusinessRules _leaveUsageBusinessRules;

        public GetByIdLeaveUsageQueryHandler(IMapper mapper, ILeaveUsageRepository leaveUsageRepository, LeaveUsageBusinessRules leaveUsageBusinessRules)
        {
            _mapper = mapper;
            _leaveUsageRepository = leaveUsageRepository;
            _leaveUsageBusinessRules = leaveUsageBusinessRules;
        }

        public async Task<GetByIdLeaveUsageResponse> Handle(GetByIdLeaveUsageQuery request, CancellationToken cancellationToken)
        {
            LeaveUsage? leaveUsage = await _leaveUsageRepository.GetAsync(predicate: lu => lu.Id == request.Id, cancellationToken: cancellationToken);
            await _leaveUsageBusinessRules.LeaveUsageShouldExistWhenSelected(leaveUsage);

            GetByIdLeaveUsageResponse response = _mapper.Map<GetByIdLeaveUsageResponse>(leaveUsage);
            return response;
        }
    }
}
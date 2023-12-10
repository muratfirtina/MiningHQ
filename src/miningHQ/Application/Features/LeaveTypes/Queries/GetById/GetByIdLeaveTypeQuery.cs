using Application.Features.LeaveTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LeaveTypes.Constants.LeaveTypesOperationClaims;

namespace Application.Features.LeaveUsages.Queries.GetById;

public class GetByIdLeaveTypeQuery : IRequest<GetByIdLeaveTypeResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLeaveUsageQueryHandler : IRequestHandler<GetByIdLeaveTypeQuery, GetByIdLeaveTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly LeaveUsageTypeBusinessRules _leaveUsageTypeBusinessRules;

        public GetByIdLeaveUsageQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, LeaveUsageTypeBusinessRules leaveUsageTypeBusinessRules)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveUsageTypeBusinessRules = leaveUsageTypeBusinessRules;
        }

        public async Task<GetByIdLeaveTypeResponse> Handle(GetByIdLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            LeaveType? leaveUsage = await _leaveTypeRepository.GetAsync(predicate: lu => lu.Id == request.Id, cancellationToken: cancellationToken);
            await _leaveUsageTypeBusinessRules.LeaveUsageShouldExistWhenSelected(leaveUsage);

            GetByIdLeaveTypeResponse response = _mapper.Map<GetByIdLeaveTypeResponse>(leaveUsage);
            return response;
        }
    }
}
using Application.Features.LeaveTypes.Constants;
using Application.Features.LeaveTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.LeaveTypes.Constants.LeaveTypesOperationClaims;

namespace Application.Features.LeaveTypes.Commands.Update;

public class UpdateLeaveTypeCommand : IRequest<UpdatedLeaveTypeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; }
    public EmployeeLeaveUsage EmployeeLeaveUsage { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }

    public string[] Roles => new[] { Admin, Write, LeaveTypesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetLeaveUsages"};

    public class UpdateLeaveUsageCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, UpdatedLeaveTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly LeaveUsageTypeBusinessRules _leaveUsageTypeBusinessRules;

        public UpdateLeaveUsageCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
                                         LeaveUsageTypeBusinessRules leaveUsageTypeBusinessRules)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveUsageTypeBusinessRules = leaveUsageTypeBusinessRules;
        }

        public async Task<UpdatedLeaveTypeResponse> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            LeaveType? leaveUsage = await _leaveTypeRepository.GetAsync(predicate: lu => lu.Id == request.Id, cancellationToken: cancellationToken);
            await _leaveUsageTypeBusinessRules.LeaveUsageShouldExistWhenSelected(leaveUsage);
            leaveUsage = _mapper.Map(request, leaveUsage);

            await _leaveTypeRepository.UpdateAsync(leaveUsage!);

            UpdatedLeaveTypeResponse response = _mapper.Map<UpdatedLeaveTypeResponse>(leaveUsage);
            return response;
        }
    }
}
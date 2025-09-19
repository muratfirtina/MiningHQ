using Application.Features.LeaveTypes.Constants;
using Application.Features.LeaveTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.LeaveTypes.Constants.LeaveTypesOperationClaims;

namespace Application.Features.LeaveTypes.Commands.Create;

public class CreateLeaveTypeCommand : IRequest<CreatedLeaveTypeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    //public Guid Id { get; set; }
    public string Name { get; set; }
    //public Guid EmployeeLeaveId { get; set; }
    //public EmployeeLeaveUsage EmployeeLeaveUsage { get; set; }
    //public DateTime? UsageDate { get; set; }
    //public DateTime? ReturnDate { get; set; }

    public string[] Roles => new[] { Admin, Write, LeaveTypesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetLeaveUsages"};

    public class CreateLeaveUsageCommandHandler : IRequestHandler<CreateLeaveTypeCommand, CreatedLeaveTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly LeaveUsageTypeBusinessRules _leaveUsageTypeBusinessRules;

        public CreateLeaveUsageCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
                                         LeaveUsageTypeBusinessRules leaveUsageTypeBusinessRules)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveUsageTypeBusinessRules = leaveUsageTypeBusinessRules;
        }

        public async Task<CreatedLeaveTypeResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            LeaveType leaveType = _mapper.Map<LeaveType>(request);

            await _leaveTypeRepository.AddAsync(leaveType);

            CreatedLeaveTypeResponse response = _mapper.Map<CreatedLeaveTypeResponse>(leaveType);
            return response;
        }
    }
}
using Application.Features.LeaveTypes.Constants;
using Application.Features.LeaveTypes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.LeaveTypes.Constants.LeaveTypesOperationClaims;

namespace Application.Features.LeaveTypes.Commands.Delete;

public class DeleteLeaveTypeCommand : IRequest<DeletedLeaveTypeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LeaveTypesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetLeaveUsages"};

    public class DeleteLeaveUsageCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, DeletedLeaveTypeResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly LeaveUsageTypeBusinessRules _leaveUsageTypeBusinessRules;

        public DeleteLeaveUsageCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository,
                                         LeaveUsageTypeBusinessRules leaveUsageTypeBusinessRules)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveUsageTypeBusinessRules = leaveUsageTypeBusinessRules;
        }

        public async Task<DeletedLeaveTypeResponse> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            LeaveType? leaveUsage = await _leaveTypeRepository.GetAsync(predicate: lu => lu.Id == request.Id, cancellationToken: cancellationToken);
            await _leaveUsageTypeBusinessRules.LeaveUsageShouldExistWhenSelected(leaveUsage);

            await _leaveTypeRepository.DeleteAsync(leaveUsage!);

            DeletedLeaveTypeResponse response = _mapper.Map<DeletedLeaveTypeResponse>(leaveUsage);
            return response;
        }
    }
}
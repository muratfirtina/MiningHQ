using Application.Features.EmployeeLeaveUsages.Constants;
using Application.Features.EmployeeLeaveUsages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.EmployeeLeaveUsages.Constants.EmployeeLeaveUsagesOperationClaims;

namespace Application.Features.EmployeeLeaveUsages.Commands.Delete;

public class DeleteEmployeeLeaveUsageCommand : IRequest<DeletedEmployeeLeaveUsageResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, EmployeeLeaveUsagesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new [] {"GetEmployeeLeaves"};

    public class DeleteEmployeeLeaveCommandHandler : IRequestHandler<DeleteEmployeeLeaveUsageCommand, DeletedEmployeeLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;
        private readonly EmployeeLeaveUsageBusinessRules _employeeLeaveUsageBusinessRules;

        public DeleteEmployeeLeaveCommandHandler(IMapper mapper, IEmployeeLeaveUsageRepository employeeLeaveUsageRepository,
                                         EmployeeLeaveUsageBusinessRules employeeLeaveUsageBusinessRules)
        {
            _mapper = mapper;
            _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
            _employeeLeaveUsageBusinessRules = employeeLeaveUsageBusinessRules;
        }

        public async Task<DeletedEmployeeLeaveUsageResponse> Handle(DeleteEmployeeLeaveUsageCommand request, CancellationToken cancellationToken)
        {
            EmployeeLeaveUsage? employeeLeave = await _employeeLeaveUsageRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _employeeLeaveUsageBusinessRules.EmployeeLeaveShouldExistWhenSelected(employeeLeave);

            await _employeeLeaveUsageRepository.DeleteAsync(employeeLeave!);

            DeletedEmployeeLeaveUsageResponse response = _mapper.Map<DeletedEmployeeLeaveUsageResponse>(employeeLeave);
            return response;
        }
    }
}
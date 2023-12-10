using Application.Features.EmployeeLeaveUsages.Constants;
using Application.Features.EmployeeLeaveUsages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.EmployeeLeaveUsages.Constants.EmployeeLeaveUsagesOperationClaims;

namespace Application.Features.EmployeeLeaveUsages.Commands.Create;

public class CreateEmployeeLeaveUsageCommand : IRequest<CreatedEmployeeLeaveUsageResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime UsageDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public Guid LeaveTypeId { get; set; }
    public int UsedDays { get; set; }

    public string[] Roles => new[] { Admin, Write, EmployeeLeaveUsagesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetEmployeeLeaves"};

    public class CreateEmployeeLeaveCommandHandler : IRequestHandler<CreateEmployeeLeaveUsageCommand, CreatedEmployeeLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;
        private readonly EmployeeLeaveUsageBusinessRules _employeeLeaveUsageBusinessRules;

        public CreateEmployeeLeaveCommandHandler(IMapper mapper, IEmployeeLeaveUsageRepository employeeLeaveUsageRepository,
                                         EmployeeLeaveUsageBusinessRules employeeLeaveUsageBusinessRules)
        {
            _mapper = mapper;
            _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
            _employeeLeaveUsageBusinessRules = employeeLeaveUsageBusinessRules;
        }

        public async Task<CreatedEmployeeLeaveUsageResponse> Handle(CreateEmployeeLeaveUsageCommand request, CancellationToken cancellationToken)
        {
            EmployeeLeaveUsage employeeLeaveUsage = _mapper.Map<EmployeeLeaveUsage>(request);
            

            await _employeeLeaveUsageRepository.AddAsync(employeeLeaveUsage);

            CreatedEmployeeLeaveUsageResponse response = _mapper.Map<CreatedEmployeeLeaveUsageResponse>(employeeLeaveUsage);
            return response;
        }
    }
}
using Application.Features.EmployeeLeaveUsages.Constants;
using Application.Features.EmployeeLeaveUsages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.EmployeeLeaveUsages.Constants.EmployeeLeaveUsagesOperationClaims;

namespace Application.Features.EmployeeLeaveUsages.Commands.Update;

public class UpdateEmployeeLeaveUsageCommand : IRequest<UpdatedEmployeeLeaveUsageResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; }

    public string[] Roles => new[] { Admin, Write, EmployeeLeaveUsagesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetEmployeeLeaves"};

    public class UpdateEmployeeLeaveCommandHandler : IRequestHandler<UpdateEmployeeLeaveUsageCommand, UpdatedEmployeeLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;
        private readonly EmployeeLeaveUsageBusinessRules _employeeLeaveUsageBusinessRules;

        public UpdateEmployeeLeaveCommandHandler(IMapper mapper, IEmployeeLeaveUsageRepository employeeLeaveUsageRepository,
                                         EmployeeLeaveUsageBusinessRules employeeLeaveUsageBusinessRules)
        {
            _mapper = mapper;
            _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
            _employeeLeaveUsageBusinessRules = employeeLeaveUsageBusinessRules;
        }

        public async Task<UpdatedEmployeeLeaveUsageResponse> Handle(UpdateEmployeeLeaveUsageCommand request, CancellationToken cancellationToken)
        {
            EmployeeLeaveUsage? employeeLeave = await _employeeLeaveUsageRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _employeeLeaveUsageBusinessRules.EmployeeLeaveShouldExistWhenSelected(employeeLeave);
            employeeLeave = _mapper.Map(request, employeeLeave);

            await _employeeLeaveUsageRepository.UpdateAsync(employeeLeave!);

            UpdatedEmployeeLeaveUsageResponse usageResponse = _mapper.Map<UpdatedEmployeeLeaveUsageResponse>(employeeLeave);
            return usageResponse;
        }
    }
}
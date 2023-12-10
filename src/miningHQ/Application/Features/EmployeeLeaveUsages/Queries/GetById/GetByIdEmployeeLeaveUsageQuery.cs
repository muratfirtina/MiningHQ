using Application.Features.EmployeeLeaveUsages.Queries.GetById;
using Application.Features.EmployeeLeaveUsages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using static Application.Features.EmployeeLeaveUsages.Constants.EmployeeLeaveUsagesOperationClaims;

namespace Application.Features.EmployeeLeaveUsages.Queries.GetById;

public class GetByIdEmployeeLeaveUsageQuery : IRequest<GetByIdEmployeeLeaveUsageResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdEmployeeLeaveQueryHandler : IRequestHandler<GetByIdEmployeeLeaveUsageQuery, GetByIdEmployeeLeaveUsageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;
        private readonly EmployeeLeaveUsageBusinessRules _employeeLeaveUsageBusinessRules;

        public GetByIdEmployeeLeaveQueryHandler(IMapper mapper, IEmployeeLeaveUsageRepository employeeLeaveUsageRepository, EmployeeLeaveUsageBusinessRules employeeLeaveUsageBusinessRules)
        {
            _mapper = mapper;
            _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
            _employeeLeaveUsageBusinessRules = employeeLeaveUsageBusinessRules;
        }

        public async Task<GetByIdEmployeeLeaveUsageResponse> Handle(GetByIdEmployeeLeaveUsageQuery request, CancellationToken cancellationToken)
        {
            EmployeeLeaveUsage? employeeLeave = await _employeeLeaveUsageRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _employeeLeaveUsageBusinessRules.EmployeeLeaveShouldExistWhenSelected(employeeLeave);

            GetByIdEmployeeLeaveUsageResponse usageResponse = _mapper.Map<GetByIdEmployeeLeaveUsageResponse>(employeeLeave);
            return usageResponse;
        }
    }
}
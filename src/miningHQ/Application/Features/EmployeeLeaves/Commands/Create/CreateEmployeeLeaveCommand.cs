using Application.Features.EmployeeLeaves.Constants;
using Application.Features.EmployeeLeaves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.EmployeeLeaves.Constants.EmployeeLeavesOperationClaims;

namespace Application.Features.EmployeeLeaves.Commands.Create;

public class CreateEmployeeLeaveCommand : IRequest<CreatedEmployeeLeaveResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; }

    public string[] Roles => new[] { Admin, Write, EmployeeLeavesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetEmployeeLeaves"};

    public class CreateEmployeeLeaveCommandHandler : IRequestHandler<CreateEmployeeLeaveCommand, CreatedEmployeeLeaveResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
        private readonly EmployeeLeaveBusinessRules _employeeLeaveBusinessRules;

        public CreateEmployeeLeaveCommandHandler(IMapper mapper, IEmployeeLeaveRepository employeeLeaveRepository,
                                         EmployeeLeaveBusinessRules employeeLeaveBusinessRules)
        {
            _mapper = mapper;
            _employeeLeaveRepository = employeeLeaveRepository;
            _employeeLeaveBusinessRules = employeeLeaveBusinessRules;
        }

        public async Task<CreatedEmployeeLeaveResponse> Handle(CreateEmployeeLeaveCommand request, CancellationToken cancellationToken)
        {
            EmployeeLeave employeeLeave = _mapper.Map<EmployeeLeave>(request);

            await _employeeLeaveRepository.AddAsync(employeeLeave);

            CreatedEmployeeLeaveResponse response = _mapper.Map<CreatedEmployeeLeaveResponse>(employeeLeave);
            return response;
        }
    }
}
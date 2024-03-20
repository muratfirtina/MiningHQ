using Application.Features.Employees.Constants;
using Application.Features.Employees.Dtos;
using Application.Features.Employees.Rules;
using Application.Services.Jobs;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Commands.Create;

public class CreateEmployeeCommand : IRequest<CreatedEmployeeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? DepartmentId { get; set; }
    public string? JobId { get; set; }
    public string? QuarryId { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? HireDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public string? LicenseType { get; set; }
    public string? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
    

    public string[] Roles => new[] { Admin, Write, EmployeesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetEmployees"};

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, CreatedEmployeeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IJobRepository _jobRepository;

        public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository, IJobRepository jobRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _jobRepository = jobRepository;
        }

        public async Task<CreatedEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {

            if (request.JobId != null)
            {
                var job =await _jobRepository.GetAsync(job => job.Id.ToString() == request.JobId);
                if (job != null)
                {
                    request.DepartmentId = job.DepartmentId.ToString();
                }
            }
            
            Employee employee = _mapper.Map<Employee>(request);

            await _employeeRepository.AddAsync(employee);

            CreatedEmployeeResponse response = _mapper.Map<CreatedEmployeeResponse>(employee);
            return response;
        }
    }
}
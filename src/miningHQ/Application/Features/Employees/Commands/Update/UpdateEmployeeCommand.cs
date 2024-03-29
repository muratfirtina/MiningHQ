using System;
using Application.Features.Employees.Constants;
using Application.Features.Employees.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Commands.Update;

public class UpdateEmployeeCommand : IRequest<UpdatedEmployeeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
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

    public string[] Roles => new[] { Admin, Write, EmployeesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetEmployees"};

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, UpdatedEmployeeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeBusinessRules _employeeBusinessRules;
        private readonly IJobRepository _jobRepository;

        public UpdateEmployeeCommandHandler(IMapper mapper, IEmployeeRepository employeeRepository,
                                         EmployeeBusinessRules employeeBusinessRules, IJobRepository jobRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeeBusinessRules = employeeBusinessRules;
            _jobRepository = jobRepository;
        }

        public async Task<UpdatedEmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            Employee? employee = await _employeeRepository.GetAsync(predicate: e => e.Id == request.Id, cancellationToken: cancellationToken);
            //Eğer job id değiştirildiyse yeni department id alınır.
            if (request.JobId != null)
            {
                Job? job = await _jobRepository.GetJobByIdWithDepartmentIdAsync(request.JobId);
                if (job != null)
                {
                    request.DepartmentId = job.DepartmentId.ToString();
                }
            }
            await _employeeBusinessRules.EmployeeShouldExistWhenSelected(employee);
            employee = _mapper.Map(request, employee);

            await _employeeRepository.UpdateAsync(employee!);

            UpdatedEmployeeResponse response = _mapper.Map<UpdatedEmployeeResponse>(employee);
            return response;
        }
    }
}
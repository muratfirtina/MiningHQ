using Application.Features.Employees.Constants;
using Application.Features.Employees.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Queries.GetById;

public class GetByIdEmployeeQuery : IRequest<GetByIdEmployeeResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdEmployeeQueryHandler : IRequestHandler<GetByIdEmployeeQuery, GetByIdEmployeeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeBusinessRules _employeeBusinessRules;

        public GetByIdEmployeeQueryHandler(IMapper mapper, IEmployeeRepository employeeRepository, EmployeeBusinessRules employeeBusinessRules)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _employeeBusinessRules = employeeBusinessRules;
        }

        public async Task<GetByIdEmployeeResponse> Handle(GetByIdEmployeeQuery request, CancellationToken cancellationToken)
        {
            Employee? employee = await _employeeRepository.GetAsync
                (predicate: e => e.Id == request.Id,
                    include: e=>e.Include(e=>e.Job)
                        .Include(e=>e.Quarry)
                        .Include(e=>e.Department)
                        .Include(e=>e.EntitledLeaves).ThenInclude(el=>el.LeaveType)
                        .Include(e=>e.EmployeeLeaveUsages).ThenInclude(el=>el.LeaveType) ,cancellationToken: cancellationToken);
            
            
            //hakedilen izinleri topla
            var totalEntitledDays = employee.EntitledLeaves.Sum(el => el.EntitledDays);
            //kullanılan izinleri topla
            var totalUsedDays = employee.EmployeeLeaveUsages.Sum(el => el.UsedDays);
            
            //hakedilen izinlerden kullanılan izinleri çıkar
            var currentLeaveDays =  totalEntitledDays - totalUsedDays;
            
            
            await _employeeBusinessRules.EmployeeShouldExistWhenSelected(employee);

            GetByIdEmployeeResponse response = _mapper.Map<GetByIdEmployeeResponse>(employee);
            response.TotalUsedLeaveDays = totalUsedDays;
            response.TotalEntitledLeaveDays = totalEntitledDays;
            response.CurrentLeaveDays = currentLeaveDays;
            
            return response;
        }
    }
}
using Application.Features.Employees.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Queries.GetList;

public class GetListEmployeeQuery : IRequest<GetListResponse<GetListEmployeeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListEmployees({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetEmployees";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListEmployeeQueryHandler : IRequestHandler<GetListEmployeeQuery, GetListResponse<GetListEmployeeListItemDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetListEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEmployeeListItemDto>> Handle(GetListEmployeeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Employee> employees = await _employeeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEmployeeListItemDto> response = _mapper.Map<GetListResponse<GetListEmployeeListItemDto>>(employees);
            return response;
        }
    }
}
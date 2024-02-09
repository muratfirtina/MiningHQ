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
using Microsoft.EntityFrameworkCore;
using static Application.Features.Employees.Constants.EmployeesOperationClaims;

namespace Application.Features.Employees.Queries.GetList;

public class GetListEmployeeQuery : IRequest<GetListResponse<GetListEmployeeListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    
    public GetListEmployeeQuery(int pageIndex = 0, int pageSize = 10)
    {
        PageRequest = new PageRequest { PageIndex = pageIndex, PageSize = pageSize };
    }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListEmployees({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey => new[] {"GetEmployees"};
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
            if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                
                var allEmployees = await _employeeRepository.GetAllAsync(
                    //employee adına göre sıralayoruz
                    orderBy: e => e.OrderBy(e => e.FirstName),
                    include:e => e.Include(e => e.Job)
                        .Include(e => e.Quarry)
                        .Include(e => e.EmployeeFiles)
                        .Include(e => e.Timekeepings)
                    );
                var employeeDtos = _mapper.Map<List<GetListEmployeeListItemDto>>(allEmployees);

                return new GetListResponse<GetListEmployeeListItemDto>
                {
                    Items = employeeDtos,
                    Index = -1,
                    Size = -1,
                    Count = allEmployees.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
            }
            else
            {
                
                IPaginate<Employee> employees = await _employeeRepository.GetListAsync(
                    orderBy: e => e.OrderBy(e => e.FirstName),
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include: e => e.Include(e => e.Job).Include(e => e.Quarry),
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListEmployeeListItemDto> response = _mapper.Map<GetListResponse<GetListEmployeeListItemDto>>(employees);
                return response;
            }
        }
    }
}
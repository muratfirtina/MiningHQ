using Application.Services.EntitledLeaves;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.EmployeeLeaveUsages.Constants.EmployeeLeaveUsagesOperationClaims;

namespace Application.Features.EmployeeLeaveUsages.Queries.GetList;

public class GetListEmployeeLeaveUsageQuery : IRequest<GetListResponse<GetListEmployeeLeaveUsageListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListEmployeeLeaves({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey => new[] {"GetEmployeeLeaves"};
    public TimeSpan? SlidingExpiration { get; }

    public class GetListEmployeeLeaveQueryHandler : IRequestHandler<GetListEmployeeLeaveUsageQuery, GetListResponse<GetListEmployeeLeaveUsageListItemDto>>
    {
        private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;
        private readonly IMapper _mapper;

        public GetListEmployeeLeaveQueryHandler(IEmployeeLeaveUsageRepository employeeLeaveUsageRepository, IMapper mapper)
        {
            _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEmployeeLeaveUsageListItemDto>> Handle(GetListEmployeeLeaveUsageQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                var allEmployeeLeaves = await _employeeLeaveUsageRepository.GetAllAsync(
                    //employee adına göre sıralayoruz
                    orderBy: e => e.OrderBy(e => e.Employee.FirstName),
                    include: e => e.Include(e => e.Employee)
                        .Include(e => e.LeaveType)
                );
                var employeeLeaveDtos = _mapper.Map<List<GetListEmployeeLeaveUsageListItemDto>>(allEmployeeLeaves);

                return new GetListResponse<GetListEmployeeLeaveUsageListItemDto>
                {
                    Items = employeeLeaveDtos,
                    Index = -1,
                    Size = -1,
                    Count = employeeLeaveDtos.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
                
            }
            else
            {
                IPaginate<EmployeeLeaveUsage> employeeLeaves = await _employeeLeaveUsageRepository.GetListAsync(
                    include: m => m.Include(c => c.Employee).Include(c => c.LeaveType),
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );
                
                GetListResponse<GetListEmployeeLeaveUsageListItemDto> response = _mapper.Map<GetListResponse<GetListEmployeeLeaveUsageListItemDto>>(employeeLeaves);
                return response;
            }
            
        }
    }
}
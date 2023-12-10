using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
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
            IPaginate<EmployeeLeaveUsage> employeeLeaves = await _employeeLeaveUsageRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEmployeeLeaveUsageListItemDto> response = _mapper.Map<GetListResponse<GetListEmployeeLeaveUsageListItemDto>>(employeeLeaves);
            return response;
        }
    }
}
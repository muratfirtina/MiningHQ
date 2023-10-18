using Application.Features.EmployeeLeaves.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.EmployeeLeaves.Constants.EmployeeLeavesOperationClaims;

namespace Application.Features.EmployeeLeaves.Queries.GetList;

public class GetListEmployeeLeaveQuery : IRequest<GetListResponse<GetListEmployeeLeaveListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListEmployeeLeaves({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetEmployeeLeaves";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListEmployeeLeaveQueryHandler : IRequestHandler<GetListEmployeeLeaveQuery, GetListResponse<GetListEmployeeLeaveListItemDto>>
    {
        private readonly IEmployeeLeaveRepository _employeeLeaveRepository;
        private readonly IMapper _mapper;

        public GetListEmployeeLeaveQueryHandler(IEmployeeLeaveRepository employeeLeaveRepository, IMapper mapper)
        {
            _employeeLeaveRepository = employeeLeaveRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListEmployeeLeaveListItemDto>> Handle(GetListEmployeeLeaveQuery request, CancellationToken cancellationToken)
        {
            IPaginate<EmployeeLeave> employeeLeaves = await _employeeLeaveRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListEmployeeLeaveListItemDto> response = _mapper.Map<GetListResponse<GetListEmployeeLeaveListItemDto>>(employeeLeaves);
            return response;
        }
    }
}
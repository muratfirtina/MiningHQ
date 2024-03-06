using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.EmployeeLeaveUsages.Queries.GetListByDynamic;

public class GetListByDynamicEmployeeLeaveUsageListItemQuery : IRequest<GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto>>
{
    public DynamicQuery DynamicQuery { get; set; }
    public PageRequest PageRequest { get; set; }
    
}

public class GetListByDynamicEmplooyeLeaveUsageListItemQueryHandler : IRequestHandler<GetListByDynamicEmployeeLeaveUsageListItemQuery, GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto>>
{
    private readonly IEmployeeLeaveUsageRepository _employeeLeaveUsageRepository;
    private readonly IEntitledLeaveRepository _entitledLeaveRepository;
    private readonly IMapper _mapper;

    public GetListByDynamicEmplooyeLeaveUsageListItemQueryHandler(IEmployeeLeaveUsageRepository employeeLeaveUsageRepository, IMapper mapper, IEntitledLeaveRepository entitledLeaveRepository)
    {
        _employeeLeaveUsageRepository = employeeLeaveUsageRepository;
        _mapper = mapper;
        _entitledLeaveRepository = entitledLeaveRepository;
    }

    public async Task<GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto>> Handle(
        GetListByDynamicEmployeeLeaveUsageListItemQuery request, CancellationToken cancellationToken)
    {
        IPaginate<EmployeeLeaveUsage> employeeLeaveUsages = await _employeeLeaveUsageRepository.GetListByDynamicAsync(
            request.DynamicQuery,
            include: e => e.Include(e => e.Employee).Include(e => e.LeaveType),
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            cancellationToken: cancellationToken);
        GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto>>(employeeLeaveUsages);
        return response;
        
    }
}

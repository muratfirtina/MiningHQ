using Application.Services.EntitledLeaves;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.EntitledLeaves.Queries.GetListByDynamic;

public class GetListByDynamicEntitledLeavesQuery: IRequest<GetListResponse<GetListByDynamicEntitledLeavesListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }
}

public class GetListByDynamicEntitledLeavesQueryHandler : IRequestHandler<GetListByDynamicEntitledLeavesQuery,
    GetListResponse<GetListByDynamicEntitledLeavesListItemDto>>
{
    private readonly IEntitledLeaveRepository _entitledLeaveRepository;
    private readonly IEntitledLeavesService _entitledLeavesService;
    private readonly IMapper _mapper;

    public GetListByDynamicEntitledLeavesQueryHandler(IEntitledLeaveRepository entitledLeaveRepository, IMapper mapper, IEntitledLeavesService entitledLeavesService)
    {
        _entitledLeaveRepository = entitledLeaveRepository;
        _mapper = mapper;
        _entitledLeavesService = entitledLeavesService;
    }

    public async Task<GetListResponse<GetListByDynamicEntitledLeavesListItemDto>> Handle(GetListByDynamicEntitledLeavesQuery request,
        CancellationToken cancellationToken)
    {
        IPaginate<EntitledLeave> entitledLeaves = await _entitledLeaveRepository.GetListByDynamicAsync(
            request.DynamicQuery,
            include: e => e.Include(e => e.Employee).Include(e => e.LeaveType),
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            cancellationToken: cancellationToken);
        
        GetListResponse<GetListByDynamicEntitledLeavesListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicEntitledLeavesListItemDto>>(entitledLeaves);
        return response;
        
    }
}
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Overtimes.Queries.GetListByDynamic;

public class GetListByDynamicOvertimeQuery: IRequest<GetListResponse<GetListByDynamicOvertimeListItemDto>>
{
    public PageRequest? PageRequest { get; set; }
    public DynamicQuery? DynamicQuery { get; set; }
}

public class GetListByDynamicOvertimeQueryHandler : IRequestHandler<GetListByDynamicOvertimeQuery,
    GetListResponse<GetListByDynamicOvertimeListItemDto>>
{
    private readonly IOvertimeRepository _overtimeRepository;
    private readonly IMapper _mapper;

    public GetListByDynamicOvertimeQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
    {
        _overtimeRepository = overtimeRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListByDynamicOvertimeListItemDto>> Handle(GetListByDynamicOvertimeQuery request, CancellationToken cancellationToken)
    {
        
        IPaginate<Overtime> overtimes = await _overtimeRepository.GetListByDynamicAsync(
            request.DynamicQuery,
            include: o => o.Include(o => o.Employee),
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize,
            cancellationToken: cancellationToken
        );
        
        GetListResponse<GetListByDynamicOvertimeListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicOvertimeListItemDto>>(overtimes);
        return response;

        
    }
}
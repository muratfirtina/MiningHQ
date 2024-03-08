using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Overtimes.Queries.GetOvertimeListByEmployeeId;

public class GetOvertimeListByEmployeeIdQuery: IRequest<GetListResponse<GetOvertimeListByEmployeeIdListItemDto>>
{
    public PageRequest? PageRequest { get; set; }
    public string? EmployeeId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

public class GetOvertimeListByEmployeeIdQueryHandler : IRequestHandler<GetOvertimeListByEmployeeIdQuery, GetListResponse<GetOvertimeListByEmployeeIdListItemDto>>
{
    private readonly IOvertimeRepository _overtimeRepository;
    private readonly IMapper _mapper;

    public GetOvertimeListByEmployeeIdQueryHandler(IOvertimeRepository overtimeRepository, IMapper mapper)
    {
        _overtimeRepository = overtimeRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetOvertimeListByEmployeeIdListItemDto>> Handle(GetOvertimeListByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        
        IPaginate<Overtime> overtimes = await _overtimeRepository.GetListAsync(
            include: o => o.Include(o => o.Employee),
            index: request.PageRequest!.PageIndex,
            size: request.PageRequest!.PageSize,
            predicate: o => o.EmployeeId.ToString() == request.EmployeeId && o.OvertimeDate >= request.StartDate && o.OvertimeDate <= request.EndDate,
            cancellationToken: cancellationToken
        );
        var totalOvertimeHours = await _overtimeRepository.GetTotalOvertimeHoursByEmployeeId(request.EmployeeId, request.StartDate, request.EndDate);

        if (request.StartDate == null && request.EndDate == null)
        {
            overtimes = await _overtimeRepository.GetListAsync(
                include: o => o.Include(o => o.Employee),
                index: request.PageRequest!.PageIndex,
                size: request.PageRequest!.PageSize,
                predicate: o => o.EmployeeId.ToString() == request.EmployeeId,
                cancellationToken: cancellationToken
            );
        }
        else if (request.StartDate != null && request.EndDate == null)
        {
            overtimes = await _overtimeRepository.GetListAsync(
                include: o => o.Include(o => o.Employee),
                index: request.PageRequest!.PageIndex,
                size: request.PageRequest!.PageSize,
                predicate: o => o.EmployeeId.ToString() == request.EmployeeId && o.OvertimeDate >= request.StartDate,
                cancellationToken: cancellationToken
            );
        }
        else
        {
            overtimes = await _overtimeRepository.GetListAsync(
                include: o => o.Include(o => o.Employee),
                index: request.PageRequest!.PageIndex,
                size: request.PageRequest!.PageSize,
                predicate: o => o.EmployeeId.ToString() == request.EmployeeId && o.OvertimeDate >= request.StartDate && o.OvertimeDate <= request.EndDate,
                cancellationToken: cancellationToken
            );
        }
        

        GetListResponse<GetOvertimeListByEmployeeIdListItemDto> response = _mapper.Map<GetListResponse<GetOvertimeListByEmployeeIdListItemDto>>(overtimes);
        
        foreach (var item in response.Items)
        {
            item.TotalOvertimeHours = totalOvertimeHours;
        }
        return response;
        
        
        
        
        

        
        
    }
}

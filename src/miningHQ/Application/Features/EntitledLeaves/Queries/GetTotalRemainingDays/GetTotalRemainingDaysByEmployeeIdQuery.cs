using Application.Services.EntitledLeaves;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;

namespace Application.Features.EntitledLeaves.Queries.GetTotalRemainingDays;

public class GetTotalRemainingDaysByEmployeeIdQuery : IRequest<GetTotalRemainingDaysByEmployeeIdDto>
{
    public string EmployeeId { get; set; }
    
}

public class GetTotalRemainingDaysByEmployeeIdQueryHandler : IRequestHandler<GetTotalRemainingDaysByEmployeeIdQuery, GetTotalRemainingDaysByEmployeeIdDto>
{
    private readonly IEntitledLeaveRepository _entitledLeaveRepository;
    private readonly IEntitledLeavesService _entitledLeavesService;
    private readonly IMapper _mapper;

    public GetTotalRemainingDaysByEmployeeIdQueryHandler(IEntitledLeaveRepository entitledLeaveRepository, IEntitledLeavesService entitledLeavesService, IMapper mapper)
    {
        _entitledLeaveRepository = entitledLeaveRepository;
        _entitledLeavesService = entitledLeavesService;
        _mapper = mapper;
    }

    public async Task<GetTotalRemainingDaysByEmployeeIdDto> Handle(GetTotalRemainingDaysByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var totalRemainingDays = await _entitledLeavesService.GetRemainingEntitledLeavesAsync(request.EmployeeId);
        
        return new GetTotalRemainingDaysByEmployeeIdDto
        {
            EmployeeId = request.EmployeeId,
            TotalRemainingDays = totalRemainingDays
        };
        
    }
}

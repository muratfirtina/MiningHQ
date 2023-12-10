using Application.Features.EntitledLeaves.Dtos;
using Application.Features.EntitledLeaves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.EntitledLeaves.Queries.GetByEmployeeId;

public class GetEntitledLeavesByEmployeeIdQuery: IRequest<GetEntitledLeavesByEmployeeIdResponse>
{
    public Guid EmployeeId { get; set; }
    public Guid? LeaveTypeId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public class GetEntitledLeavesByEmployeeIdQueryHandler : IRequestHandler<GetEntitledLeavesByEmployeeIdQuery, GetEntitledLeavesByEmployeeIdResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEntitledLeaveRepository _entitledLeaveRepository;
        private readonly EntitledLeaveBusinessRules _entitledLeaveBusinessRules;

        public GetEntitledLeavesByEmployeeIdQueryHandler(IMapper mapper, IEntitledLeaveRepository entitledLeaveRepository, EntitledLeaveBusinessRules entitledLeaveBusinessRules)
        {
            _mapper = mapper;
            _entitledLeaveRepository = entitledLeaveRepository;
            _entitledLeaveBusinessRules = entitledLeaveBusinessRules;
        }

        public async Task<GetEntitledLeavesByEmployeeIdResponse> Handle(GetEntitledLeavesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            List<EntitledLeave> entitledLeaves = await _entitledLeaveRepository.GetAllAsync(
                predicate: el => el.EmployeeId == request.EmployeeId 
                                 && (request.LeaveTypeId == null || el.LeaveTypeId == request.LeaveTypeId)
                                 && (request.StartDate == null || el.EntitledDate >= request.StartDate)
                                 && (request.EndDate == null || el.EntitledDate <= request.EndDate),
                include: el => el.Include(el => el.LeaveType).Include(el => el.Employee),
                cancellationToken: cancellationToken
            );
            
            List<EmployeeEntitledLeaveDto> employeeEntitledLeaves = _mapper.Map<List<EmployeeEntitledLeaveDto>>(entitledLeaves);

            return new GetEntitledLeavesByEmployeeIdResponse
            {
                EntitledLeaves = employeeEntitledLeaves
            };
        }
    }
}
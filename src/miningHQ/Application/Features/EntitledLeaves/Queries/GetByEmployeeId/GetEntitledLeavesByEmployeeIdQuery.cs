using Application.Features.EntitledLeaves.Dtos;
using Application.Features.EntitledLeaves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.EntitledLeaves.Queries.GetByEmployeeId;

public class GetEntitledLeavesByEmployeeIdQuery : IRequest<GetListResponse<GetEmployeeEntitledLeaveDto>>
{
    public Guid EmployeeId { get; set; }
    public Guid? LeaveTypeId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public PageRequest PageRequest { get; set; }
    
    public class GetEntitledLeavesByEmployeeIdQueryHandler : IRequestHandler<GetEntitledLeavesByEmployeeIdQuery, GetListResponse<GetEmployeeEntitledLeaveDto>>
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

        public async Task<GetListResponse<GetEmployeeEntitledLeaveDto>> Handle(GetEntitledLeavesByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {

                var entitledLeaves = await _entitledLeaveRepository.GetAllAsync(
                    predicate: el => el.EmployeeId == request.EmployeeId 
                                     && (request.LeaveTypeId == null || el.LeaveTypeId == request.LeaveTypeId)
                                     && (request.StartDate == null || el.EntitledDate >= request.StartDate)
                                     && (request.EndDate == null || el.EntitledDate <= request.EndDate),
                    include: el => el.Include(el => el.LeaveType).Include(el => el.Employee),
                    cancellationToken: cancellationToken
                );
                
                var employeeEntitledLeaves = _mapper.Map<List<GetEmployeeEntitledLeaveDto>>(entitledLeaves);

                return new GetListResponse<GetEmployeeEntitledLeaveDto>
                {
                    Items = employeeEntitledLeaves,
                    Index = -1,
                    Size = -1,
                    Count = entitledLeaves.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
                
            }
            else
            {
                IPaginate<EntitledLeave> entitledLeaves = await _entitledLeaveRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    predicate: el => el.EmployeeId == request.EmployeeId 
                                     && (request.LeaveTypeId == null || el.LeaveTypeId == request.LeaveTypeId)
                                     && (request.StartDate == null || el.EntitledDate >= request.StartDate)
                                     && (request.EndDate == null || el.EntitledDate <= request.EndDate),
                    include: el => el.Include(el => el.LeaveType).Include(el => el.Employee),
                    cancellationToken: cancellationToken
                );
                
                var response = _mapper.Map<GetListResponse<GetEmployeeEntitledLeaveDto>>(entitledLeaves);
                return response;
            }
            /*List<EntitledLeave> entitledLeaves = await _entitledLeaveRepository.GetAllAsync(
                predicate: el => el.EmployeeId == request.EmployeeId 
                                 && (request.LeaveTypeId == null || el.LeaveTypeId == request.LeaveTypeId)
                                 && (request.StartDate == null || el.EntitledDate >= request.StartDate)
                                 && (request.EndDate == null || el.EntitledDate <= request.EndDate),
                include: el => el.Include(el => el.LeaveType).Include(el => el.Employee),
                cancellationToken: cancellationToken
            );
            
            List<EmployeeEntitledLeaveDto> employeeEntitledLeaves = _mapper.Map<List<EmployeeEntitledLeaveDto>>(entitledLeaves);

            return new GetEmployeeEntitledLeaveDto
            {
                EntitledLeaves = employeeEntitledLeaves
            };*/
        }
    }
}
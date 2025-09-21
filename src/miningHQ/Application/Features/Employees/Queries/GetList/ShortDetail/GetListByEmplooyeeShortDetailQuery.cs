using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Employees.Queries.GetList.ShortDetail;

public class GetListByEmplooyeeShortDetailQuery : IRequest<GetListResponse<GetListByEmplooyeeShortDetailItemDto>>
{
    public PageRequest PageRequest { get; set; }
}
public class GetListByEmplooyeeShortDetailQueryHandler : IRequestHandler<GetListByEmplooyeeShortDetailQuery, GetListResponse<GetListByEmplooyeeShortDetailItemDto>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    
    public GetListByEmplooyeeShortDetailQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }
    
    public async Task<GetListResponse<GetListByEmplooyeeShortDetailItemDto>> Handle(GetListByEmplooyeeShortDetailQuery request, CancellationToken cancellationToken)
    {
        if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
        {
            var allEmployees = await _employeeRepository.GetAllAsync(
                orderBy: e => e.OrderBy(e => e.FirstName),
                include:e => e.Include(e => e.Job)
                    .Include(e => e.Department)
                    .Include(e => e.Quarry)
                );

            var employeeDtos = _mapper.Map<List<GetListByEmplooyeeShortDetailItemDto>>(allEmployees);

            return new GetListResponse<GetListByEmplooyeeShortDetailItemDto>
            {
                Items = employeeDtos,
                Index = -1,
                Size = -1,
                Count = employeeDtos.Count,
                Pages = -1,
                HasPrevious = false,
                HasNext = false
            };
        }

        else
        {
            IPaginate<Employee> employees = await _employeeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: e => e.OrderBy(e => e.FirstName),
                include:e => e.Include(e => e.Job)
                    .Include(e => e.Department)
                    .Include(e => e.Quarry)
                );
            
            var response = _mapper.Map<GetListResponse<GetListByEmplooyeeShortDetailItemDto>>(employees);
            return response;
            
        }
        
    }
}
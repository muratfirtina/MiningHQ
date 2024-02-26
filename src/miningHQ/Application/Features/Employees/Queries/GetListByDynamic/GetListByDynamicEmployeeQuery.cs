using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.Employees.Queries.GetListByDynamic;

public class GetListByDynamicEmployeeQuery: IRequest<GetListResponse<GetListByDynamicEmployeeListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }
}

public class GetListByDynamicEmployeeQueryHandler : IRequestHandler<GetListByDynamicEmployeeQuery,
    GetListResponse<GetListByDynamicEmployeeListItemDto>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetListByDynamicEmployeeQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<GetListResponse<GetListByDynamicEmployeeListItemDto>> Handle(GetListByDynamicEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        
        if (request.PageRequest.PageIndex == -1&& request.PageRequest.PageSize == -1)
        {
            var allEmployees = await _employeeRepository.GetAllByDynamicAsync(
                request.DynamicQuery,
                include: e => e.Include(e => e.Job).Include(e => e.Quarry),
                cancellationToken: cancellationToken);
            
            
            var employeesDtos = _mapper.Map<List<GetListByDynamicEmployeeListItemDto>>(allEmployees);
            return new GetListResponse<GetListByDynamicEmployeeListItemDto>
            {
                Items = employeesDtos,
                Index = -1,
                Size = -1,
                Pages = -1,
                Count = allEmployees.Count,
                HasPrevious = false,
                HasNext = false
                
            };
        }
        
        
        IPaginate<Employee> employees = await _employeeRepository.GetListByDynamicAsync(
            request.DynamicQuery,
            include: e => e.Include(e => e.Job) .Include(e => e.Quarry),
            index: request.PageRequest.PageIndex,
            size: request.PageRequest.PageSize, 
            cancellationToken: cancellationToken);

        var employeeDtos = _mapper.Map<GetListResponse<GetListByDynamicEmployeeListItemDto>>(employees);
        return employeeDtos;
            
    }
}


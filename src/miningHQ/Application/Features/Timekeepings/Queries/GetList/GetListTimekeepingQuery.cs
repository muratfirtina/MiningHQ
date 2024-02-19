using Application.Features.Timekeepings.Constants;
<<<<<<< HEAD
using Application.Services.Employees;
using Application.Services.EntitledLeaves;
=======
>>>>>>> origin/main
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Timekeepings.Constants.TimekeepingsOperationClaims;

namespace Application.Features.Timekeepings.Queries.GetList;

public class GetListTimekeepingQuery : IRequest<GetListResponse<GetListTimekeepingListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListTimekeepings({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetTimekeepings";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTimekeepingQueryHandler : IRequestHandler<GetListTimekeepingQuery, GetListResponse<GetListTimekeepingListItemDto>>
    {
        private readonly ITimekeepingRepository _timekeepingRepository;
        private readonly IMapper _mapper;
<<<<<<< HEAD
        private readonly IEmployeesService _employeesService;
        private readonly IEntitledLeavesService _entitledLeavesService;

        public GetListTimekeepingQueryHandler(ITimekeepingRepository timekeepingRepository, IMapper mapper, IEmployeesService employeesService, IEntitledLeavesService entitledLeavesService)
        {
            _timekeepingRepository = timekeepingRepository;
            _mapper = mapper;
            _employeesService = employeesService;
            _entitledLeavesService = entitledLeavesService;
=======
        private readonly IEmployeeRepository _employeeRepository;

        public GetListTimekeepingQueryHandler(ITimekeepingRepository timekeepingRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _timekeepingRepository = timekeepingRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
>>>>>>> origin/main
        }

        public async Task<GetListResponse<GetListTimekeepingListItemDto>> Handle(GetListTimekeepingQuery request, CancellationToken cancellationToken)
        {
            
<<<<<<< HEAD
                
            
                var employeesWithTimekeepings = await _employeesService.GetEmployeesWithTimekeepings(request.Year, request.Month,request.PageRequest.PageIndex, request.PageRequest.PageSize);
                
                //buraya TotalRemainingDays ekle bunun için entitledLeavesService kullanılarak GetRemainingEntitledLeavesAsync metodundan yararlanılabilir.
                foreach (var employee in employeesWithTimekeepings)
                {
                    employee.TotalRemainingDays = await _entitledLeavesService.GetRemainingEntitledLeavesAsync(employee.EmployeeId.ToString());
                }
                
=======
            
                var employeesWithTimekeepings = await _employeeRepository.GetEmployeesWithTimekeepings(request.Year, request.Month,request.PageRequest.PageIndex, request.PageRequest.PageSize);
    
>>>>>>> origin/main
                // Çekilen veriyi GetListTimekeepingListItemDto'ya dönüştür
                var mappedResult = _mapper.Map<List<GetListTimekeepingListItemDto>>(employeesWithTimekeepings);
    
                // Dönüş yapısı
                return new GetListResponse<GetListTimekeepingListItemDto>
                {
                    Items = mappedResult,
                    Index = request.PageRequest.PageIndex,
                    Size = request.PageRequest.PageSize,
                    Count = mappedResult.Count,
                    HasPrevious = false,
                    HasNext = false
                };
            
            
        }
    }
}
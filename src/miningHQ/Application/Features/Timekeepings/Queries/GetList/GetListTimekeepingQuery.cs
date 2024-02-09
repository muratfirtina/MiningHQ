using Application.Features.Timekeepings.Constants;
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
        private readonly IEmployeeRepository _employeeRepository;

        public GetListTimekeepingQueryHandler(ITimekeepingRepository timekeepingRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _timekeepingRepository = timekeepingRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task<GetListResponse<GetListTimekeepingListItemDto>> Handle(GetListTimekeepingQuery request, CancellationToken cancellationToken)
        {
            
            
                var employeesWithTimekeepings = await _employeeRepository.GetEmployeesWithTimekeepings(request.Year, request.Month,request.PageRequest.PageIndex, request.PageRequest.PageSize);
    
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
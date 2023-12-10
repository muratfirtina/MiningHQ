using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using static Application.Features.LeaveTypes.Constants.LeaveTypesOperationClaims;

namespace Application.Features.LeaveTypes.Queries.GetList;

public class GetListLeaveTypeQuery : IRequest<GetListResponse<GetListLeaveTypesListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListLeaveUsages({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey =>new[] {"GetLeaveUsages"};
    public TimeSpan? SlidingExpiration { get; }

    public class GetListLeaveUsageQueryHandler : IRequestHandler<GetListLeaveTypeQuery, GetListResponse<GetListLeaveTypesListItemDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetListLeaveUsageQueryHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListLeaveTypesListItemDto>> Handle(GetListLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            if (request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                
                var allLeaveTypes = await _leaveTypeRepository.GetAllAsync(
                    //leaveType adına göre sıralayoruz
                    orderBy: e => e.OrderBy(e => e.Name)
                    );
                var leaveTypeDtos = _mapper.Map<List<GetListLeaveTypesListItemDto>>(allLeaveTypes);

                return new GetListResponse<GetListLeaveTypesListItemDto>
                {
                    Items = leaveTypeDtos,
                    Index = -1,
                    Size = -1,
                    Count = allLeaveTypes.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                    
                };
            }
            else
            {
                IPaginate<LeaveType> leaveTypes = await _leaveTypeRepository.GetListAsync(
                    orderBy: e => e.OrderBy(e => e.Name),
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                    );
                
                GetListResponse<GetListLeaveTypesListItemDto> response = _mapper.Map<GetListResponse<GetListLeaveTypesListItemDto>>(leaveTypes);
                return response;
            }

            
        }
    }
}
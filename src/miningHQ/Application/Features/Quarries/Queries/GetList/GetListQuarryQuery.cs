using Application.Features.Quarries.Constants;
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
using static Application.Features.Quarries.Constants.QuarriesOperationClaims;

namespace Application.Features.Quarries.Queries.GetList;

public class GetListQuarryQuery : IRequest<GetListResponse<GetListQuarryListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListQuarries({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey =>new[] {"GetQuarries"};
    public TimeSpan? SlidingExpiration { get; }

    public class GetListQuarryQueryHandler : IRequestHandler<GetListQuarryQuery, GetListResponse<GetListQuarryListItemDto>>
    {
        private readonly IQuarryRepository _quarryRepository;
        private readonly IMapper _mapper;

        public GetListQuarryQueryHandler(IQuarryRepository quarryRepository, IMapper mapper)
        {
            _quarryRepository = quarryRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuarryListItemDto>> Handle(GetListQuarryQuery request, CancellationToken cancellationToken)
        {
            if(request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize ==-1){

                var allQuarries = await _quarryRepository.GetAllAsync(

                );
                var quarryDto = _mapper.Map<List<GetListQuarryListItemDto>>(allQuarries);

                return new GetListResponse<GetListQuarryListItemDto>
                {
                    Items = quarryDto,
                    Index = -1,
                    Size = -1,
                    Count = allQuarries.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
                
            }
            else
            {
                IPaginate<Quarry> quarries = await _quarryRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListQuarryListItemDto> response = _mapper.Map<GetListResponse<GetListQuarryListItemDto>>(quarries);
                return response;
            }
        }
    }
}
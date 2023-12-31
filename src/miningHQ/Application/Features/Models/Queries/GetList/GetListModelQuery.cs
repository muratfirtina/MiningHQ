using Application.Features.Models.Constants;
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
using static Application.Features.Models.Constants.ModelsOperationClaims;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelQuery : IRequest<GetListResponse<GetListModelListItemDto>>//, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid? BrandId { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListModels({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string[] CacheGroupKey =>new[]{ "GetModels"};
    public TimeSpan? SlidingExpiration { get; }

    public class GetListModelQueryHandler : IRequestHandler<GetListModelQuery, GetListResponse<GetListModelListItemDto>>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public GetListModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListModelListItemDto>> Handle(GetListModelQuery request, CancellationToken cancellationToken)
        {
            if(request.PageRequest.PageIndex == -1 && request.PageRequest.PageSize == -1)
            {
                var allModels = await _modelRepository.GetAllAsync(
                    m => !request.BrandId.HasValue || m.BrandId == request.BrandId.Value,
                    include:m => m.Include(m => m.Brand)
                    );
                var modelDto = _mapper.Map<List<GetListModelListItemDto>>(allModels);

                return new GetListResponse<GetListModelListItemDto>
                {
                    Items = modelDto,
                    Index = -1,
                    Size = -1,
                    Count = allModels.Count,
                    Pages = -1,
                    HasPrevious = false,
                    HasNext = false
                };
            }
            else
            {
                IPaginate<Model> models = await _modelRepository.GetListAsync(
                    index: request.PageRequest.PageIndex,
                    size: request.PageRequest.PageSize,
                    include:m => m.Include(m => m.Brand),
                    cancellationToken: cancellationToken
                );

                GetListResponse<GetListModelListItemDto> response = _mapper.Map<GetListResponse<GetListModelListItemDto>>(models);
                return response;
                    
            }
        }
    }
}
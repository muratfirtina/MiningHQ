using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using MediatR;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Models.Queries.GetListDynamicQuery;
public class GetListByBrandIdDynamicQuery : IRequest<GetListResponse<GetListByBrandIdDynamicQueryDto>>
{
    
    public Guid BrandId { get; set; }

    public class GetListByBrandIdDynamicQueryHandler : IRequestHandler<GetListByBrandIdDynamicQuery, GetListResponse<GetListByBrandIdDynamicQueryDto>>
    {
        // Assuming these services are injected via the constructor
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public GetListByBrandIdDynamicQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByBrandIdDynamicQueryDto>> Handle(GetListByBrandIdDynamicQuery request,
            CancellationToken cancellationToken)
        {
            // Initialize the Filter property of DynamicQuery
            var filter = new Filter
            {
                Field = "BrandId",
                Operator = "eq",
                Value = request.BrandId.ToString()
            };

            // Create a new instance of DynamicQuery with the filter
            var dynamicQuery = new DynamicQuery
            {
                Filter = filter
            };
            
            
            var models = await _modelRepository.GetAllByDynamicAsync(
                predicate: m => m.BrandId == request.BrandId,
                include: m => m.Include(m => m.Brand),
                index: -1,
                size: -1,
                enableTracking: true,
                cancellationToken: cancellationToken,
                dynamic: dynamicQuery
                
            );
            var modelDtos = _mapper.Map<List<GetListByBrandIdDynamicQueryDto>>(models);

            return new GetListResponse<GetListByBrandIdDynamicQueryDto>
            {
                Items = modelDtos,
                Index = -1,
                Size = -1,
                Count = models.Count,
                Pages = -1,
                HasPrevious = false,
                HasNext = false
            };

        }

    }
}

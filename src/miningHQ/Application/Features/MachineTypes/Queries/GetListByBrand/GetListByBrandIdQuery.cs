using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MachineTypes.Queries.GetListByBrand;

public class GetListByBrandIdQuery: IRequest<GetListResponse<GetListByBrandIdDto>>
{
    public Guid BrandId { get; set; }
    
    public class GetListByBrandIdQueryHandler : IRequestHandler<GetListByBrandIdQuery, GetListResponse<GetListByBrandIdDto>>
    {
        private readonly IMachineTypeRepository _machineTypeRepository;
        private readonly IMapper _mapper;

        public GetListByBrandIdQueryHandler(IMachineTypeRepository machineTypeRepository, IMapper mapper)
        {
            _machineTypeRepository = machineTypeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByBrandIdDto>> Handle(GetListByBrandIdQuery request, CancellationToken cancellationToken)
        { 
            // MachineType'ları BrandId'ye göre filtreleyin
            var machineTypes = await _machineTypeRepository.GetAllAsync(
                include: m => m.Include(m => m.Brands),
                predicate: m => m.Brands.Any(b => b.Id == request.BrandId),
                index: -1,
                size: -1,
                enableTracking: true,
                cancellationToken: cancellationToken
            );
    
            var machineTypeDtos = _mapper.Map<List<GetListByBrandIdDto>>(machineTypes);
            return new GetListResponse<GetListByBrandIdDto>
            {
                Items = machineTypeDtos,
                Index = -1,
                Size = -1,
                Count = machineTypes.Count,
                Pages = -1,
                HasNext = false,
                HasPrevious = false        
            };
        }
    }
}
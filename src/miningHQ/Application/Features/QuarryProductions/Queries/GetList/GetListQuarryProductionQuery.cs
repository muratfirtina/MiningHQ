using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.QuarryProductions.Queries.GetList;

public class GetListQuarryProductionQuery : IRequest<GetListResponse<GetListQuarryProductionListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public Guid? QuarryId { get; set; }

    public class GetListQuarryProductionQueryHandler : IRequestHandler<GetListQuarryProductionQuery, GetListResponse<GetListQuarryProductionListItemDto>>
    {
        private readonly IQuarryProductionRepository _quarryProductionRepository;
        private readonly IMapper _mapper;

        public GetListQuarryProductionQueryHandler(IQuarryProductionRepository quarryProductionRepository, IMapper mapper)
        {
            _quarryProductionRepository = quarryProductionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuarryProductionListItemDto>> Handle(GetListQuarryProductionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<QuarryProduction> quarryProductions = await _quarryProductionRepository.GetListAsync(
                predicate: request.QuarryId.HasValue ? qp => qp.QuarryId == request.QuarryId.Value : null,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListQuarryProductionListItemDto> response = _mapper.Map<GetListResponse<GetListQuarryProductionListItemDto>>(quarryProductions);
            return response;
        }
    }
}

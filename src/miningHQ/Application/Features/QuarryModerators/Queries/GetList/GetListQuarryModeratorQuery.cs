using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.QuarryModerators.Queries.GetList;

public class GetListQuarryModeratorQuery : IRequest<GetListResponse<GetListQuarryModeratorListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; } = new();
    
    public string[] Roles => new[] { Domain.Constants.Roles.Admin };

    public class GetListQuarryModeratorQueryHandler : 
        IRequestHandler<GetListQuarryModeratorQuery, GetListResponse<GetListQuarryModeratorListItemDto>>
    {
        private readonly IQuarryModeratorRepository _quarryModeratorRepository;
        private readonly IMapper _mapper;

        public GetListQuarryModeratorQueryHandler(
            IQuarryModeratorRepository quarryModeratorRepository,
            IMapper mapper)
        {
            _quarryModeratorRepository = quarryModeratorRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListQuarryModeratorListItemDto>> Handle(
            GetListQuarryModeratorQuery request, 
            CancellationToken cancellationToken)
        {
            IPaginate<QuarryModerator> quarryModerators = await _quarryModeratorRepository.GetListAsync(
                include: query => query
                    .Include(qm => qm.User)
                    .Include(qm => qm.Quarry),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListQuarryModeratorListItemDto> response = 
                _mapper.Map<GetListResponse<GetListQuarryModeratorListItemDto>>(quarryModerators);
            
            return response;
        }
    }
}

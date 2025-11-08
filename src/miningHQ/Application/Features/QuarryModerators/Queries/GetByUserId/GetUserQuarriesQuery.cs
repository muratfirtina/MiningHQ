using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.QuarryModerators.Queries.GetByUserId;

public class GetUserQuarriesQuery : IRequest<GetUserQuarriesResponse>, ISecuredRequest
{
    public Guid UserId { get; set; }
    
    public string[] Roles => new[] { 
        Domain.Constants.Roles.Admin, 
        Domain.Constants.Roles.Moderator 
    };

    public class GetUserQuarriesQueryHandler : 
        IRequestHandler<GetUserQuarriesQuery, GetUserQuarriesResponse>
    {
        private readonly IQuarryModeratorRepository _quarryModeratorRepository;
        private readonly IMapper _mapper;

        public GetUserQuarriesQueryHandler(
            IQuarryModeratorRepository quarryModeratorRepository,
            IMapper mapper)
        {
            _quarryModeratorRepository = quarryModeratorRepository;
            _mapper = mapper;
        }

        public async Task<GetUserQuarriesResponse> Handle(
            GetUserQuarriesQuery request, 
            CancellationToken cancellationToken)
        {
            var quarryModerators = await _quarryModeratorRepository.GetListAsync(
                predicate: qm => qm.UserId == request.UserId,
                include: query => query.Include(qm => qm.Quarry),
                cancellationToken: cancellationToken
            );

            GetUserQuarriesResponse response = _mapper.Map<GetUserQuarriesResponse>(quarryModerators);
            return response;
        }
    }
}

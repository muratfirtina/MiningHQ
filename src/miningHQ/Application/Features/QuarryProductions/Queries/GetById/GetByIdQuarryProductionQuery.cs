using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.QuarryProductions.Queries.GetById;

public class GetByIdQuarryProductionQuery : IRequest<GetByIdQuarryProductionResponse>
{
    public Guid Id { get; set; }

    public class GetByIdQuarryProductionQueryHandler : IRequestHandler<GetByIdQuarryProductionQuery, GetByIdQuarryProductionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryProductionRepository _quarryProductionRepository;

        public GetByIdQuarryProductionQueryHandler(IMapper mapper, IQuarryProductionRepository quarryProductionRepository)
        {
            _mapper = mapper;
            _quarryProductionRepository = quarryProductionRepository;
        }

        public async Task<GetByIdQuarryProductionResponse> Handle(GetByIdQuarryProductionQuery request, CancellationToken cancellationToken)
        {
            QuarryProduction? quarryProduction = await _quarryProductionRepository.GetAsync(
                predicate: qp => qp.Id == request.Id,
                cancellationToken: cancellationToken
            );

            GetByIdQuarryProductionResponse response = _mapper.Map<GetByIdQuarryProductionResponse>(quarryProduction);
            return response;
        }
    }
}

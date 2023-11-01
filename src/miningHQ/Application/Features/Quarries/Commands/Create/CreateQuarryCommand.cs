using Application.Features.Quarries.Constants;
using Application.Features.Quarries.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Quarries.Constants.QuarriesOperationClaims;

namespace Application.Features.Quarries.Commands.Create;

public class CreateQuarryCommand : IRequest<CreatedQuarryResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, QuarriesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetQuarries";

    public class CreateQuarryCommandHandler : IRequestHandler<CreateQuarryCommand, CreatedQuarryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryRepository _quarryRepository;
        private readonly QuarryBusinessRules _quarryBusinessRules;

        public CreateQuarryCommandHandler(IMapper mapper, IQuarryRepository quarryRepository,
                                         QuarryBusinessRules quarryBusinessRules)
        {
            _mapper = mapper;
            _quarryRepository = quarryRepository;
            _quarryBusinessRules = quarryBusinessRules;
        }

        public async Task<CreatedQuarryResponse> Handle(CreateQuarryCommand request, CancellationToken cancellationToken)
        {
            Quarry quarry = _mapper.Map<Quarry>(request);

            await _quarryRepository.AddAsync(quarry);

            CreatedQuarryResponse response = _mapper.Map<CreatedQuarryResponse>(quarry);
            return response;
        }
    }
}
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

namespace Application.Features.Quarries.Commands.Update;

public class UpdateQuarryCommand : IRequest<UpdatedQuarryResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, QuarriesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey =>new[] {"GetQuarries"};

    public class UpdateQuarryCommandHandler : IRequestHandler<UpdateQuarryCommand, UpdatedQuarryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryRepository _quarryRepository;
        private readonly QuarryBusinessRules _quarryBusinessRules;

        public UpdateQuarryCommandHandler(IMapper mapper, IQuarryRepository quarryRepository,
                                         QuarryBusinessRules quarryBusinessRules)
        {
            _mapper = mapper;
            _quarryRepository = quarryRepository;
            _quarryBusinessRules = quarryBusinessRules;
        }

        public async Task<UpdatedQuarryResponse> Handle(UpdateQuarryCommand request, CancellationToken cancellationToken)
        {
            Quarry? quarry = await _quarryRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _quarryBusinessRules.QuarryShouldExistWhenSelected(quarry);
            quarry = _mapper.Map(request, quarry);

            await _quarryRepository.UpdateAsync(quarry!);

            UpdatedQuarryResponse response = _mapper.Map<UpdatedQuarryResponse>(quarry);
            return response;
        }
    }
}
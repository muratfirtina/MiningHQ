using Application.Features.Quarries.Constants;
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

namespace Application.Features.Quarries.Commands.Delete;

public class DeleteQuarryCommand : IRequest<DeletedQuarryResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, QuarriesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetQuarries";

    public class DeleteQuarryCommandHandler : IRequestHandler<DeleteQuarryCommand, DeletedQuarryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryRepository _quarryRepository;
        private readonly QuarryBusinessRules _quarryBusinessRules;

        public DeleteQuarryCommandHandler(IMapper mapper, IQuarryRepository quarryRepository,
                                         QuarryBusinessRules quarryBusinessRules)
        {
            _mapper = mapper;
            _quarryRepository = quarryRepository;
            _quarryBusinessRules = quarryBusinessRules;
        }

        public async Task<DeletedQuarryResponse> Handle(DeleteQuarryCommand request, CancellationToken cancellationToken)
        {
            Quarry? quarry = await _quarryRepository.GetAsync(predicate: q => q.Id == request.Id, cancellationToken: cancellationToken);
            await _quarryBusinessRules.QuarryShouldExistWhenSelected(quarry);

            await _quarryRepository.DeleteAsync(quarry!);

            DeletedQuarryResponse response = _mapper.Map<DeletedQuarryResponse>(quarry);
            return response;
        }
    }
}
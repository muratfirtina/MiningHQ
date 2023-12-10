using Application.Features.EntitledLeaves.Constants;
using Application.Features.EntitledLeaves.Constants;
using Application.Features.EntitledLeaves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.EntitledLeaves.Constants.EntitledLeavesOperationClaims;

namespace Application.Features.EntitledLeaves.Commands.Delete;

public class DeleteEntitledLeaveCommand : IRequest<DeletedEntitledLeaveResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, EntitledLeavesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetEntitledLeaves";

    public class DeleteEntitledLeaveCommandHandler : IRequestHandler<DeleteEntitledLeaveCommand, DeletedEntitledLeaveResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEntitledLeaveRepository _entitledLeaveRepository;
        private readonly EntitledLeaveBusinessRules _entitledLeaveBusinessRules;

        public DeleteEntitledLeaveCommandHandler(IMapper mapper, IEntitledLeaveRepository entitledLeaveRepository,
                                         EntitledLeaveBusinessRules entitledLeaveBusinessRules)
        {
            _mapper = mapper;
            _entitledLeaveRepository = entitledLeaveRepository;
            _entitledLeaveBusinessRules = entitledLeaveBusinessRules;
        }

        public async Task<DeletedEntitledLeaveResponse> Handle(DeleteEntitledLeaveCommand request, CancellationToken cancellationToken)
        {
            EntitledLeave? entitledLeave = await _entitledLeaveRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _entitledLeaveBusinessRules.EntitledLeaveShouldExistWhenSelected(entitledLeave);

            await _entitledLeaveRepository.DeleteAsync(entitledLeave!);

            DeletedEntitledLeaveResponse response = _mapper.Map<DeletedEntitledLeaveResponse>(entitledLeave);
            return response;
        }
    }
}
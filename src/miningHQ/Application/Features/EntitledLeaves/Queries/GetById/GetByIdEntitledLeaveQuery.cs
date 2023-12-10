using Application.Features.EntitledLeaves.Constants;
using Application.Features.EntitledLeaves.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.EntitledLeaves.Constants.EntitledLeavesOperationClaims;

namespace Application.Features.EntitledLeaves.Queries.GetById;

public class GetByIdEntitledLeaveQuery : IRequest<GetByIdEntitledLeaveResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdEntitledLeaveQueryHandler : IRequestHandler<GetByIdEntitledLeaveQuery, GetByIdEntitledLeaveResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEntitledLeaveRepository _entitledLeaveRepository;
        private readonly EntitledLeaveBusinessRules _entitledLeaveBusinessRules;

        public GetByIdEntitledLeaveQueryHandler(IMapper mapper, IEntitledLeaveRepository entitledLeaveRepository, EntitledLeaveBusinessRules entitledLeaveBusinessRules)
        {
            _mapper = mapper;
            _entitledLeaveRepository = entitledLeaveRepository;
            _entitledLeaveBusinessRules = entitledLeaveBusinessRules;
        }

        public async Task<GetByIdEntitledLeaveResponse> Handle(GetByIdEntitledLeaveQuery request, CancellationToken cancellationToken)
        {
            EntitledLeave? entitledLeave = await _entitledLeaveRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _entitledLeaveBusinessRules.EntitledLeaveShouldExistWhenSelected(entitledLeave);

            GetByIdEntitledLeaveResponse response = _mapper.Map<GetByIdEntitledLeaveResponse>(entitledLeave);
            return response;
        }
    }
}
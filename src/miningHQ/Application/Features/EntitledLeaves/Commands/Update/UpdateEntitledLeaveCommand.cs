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

namespace Application.Features.EntitledLeaves.Commands.Update;

public class UpdateEntitledLeaveCommand : IRequest<UpdatedEntitledLeaveResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public Guid LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public DateTime? EntitledDate { get; set; }

    public string[] Roles => new[] { Admin, Write, EntitledLeavesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetEntitledLeaves";

    public class UpdateEntitledLeaveCommandHandler : IRequestHandler<UpdateEntitledLeaveCommand, UpdatedEntitledLeaveResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEntitledLeaveRepository _entitledLeaveRepository;
        private readonly EntitledLeaveBusinessRules _entitledLeaveBusinessRules;

        public UpdateEntitledLeaveCommandHandler(IMapper mapper, IEntitledLeaveRepository entitledLeaveRepository,
                                         EntitledLeaveBusinessRules entitledLeaveBusinessRules)
        {
            _mapper = mapper;
            _entitledLeaveRepository = entitledLeaveRepository;
            _entitledLeaveBusinessRules = entitledLeaveBusinessRules;
        }

        public async Task<UpdatedEntitledLeaveResponse> Handle(UpdateEntitledLeaveCommand request, CancellationToken cancellationToken)
        {
            EntitledLeave? entitledLeave = await _entitledLeaveRepository.GetAsync(predicate: el => el.Id == request.Id, cancellationToken: cancellationToken);
            await _entitledLeaveBusinessRules.EntitledLeaveShouldExistWhenSelected(entitledLeave);
            entitledLeave = _mapper.Map(request, entitledLeave);

            await _entitledLeaveRepository.UpdateAsync(entitledLeave!);

            UpdatedEntitledLeaveResponse response = _mapper.Map<UpdatedEntitledLeaveResponse>(entitledLeave);
            return response;
        }
    }
}
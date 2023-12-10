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

namespace Application.Features.EntitledLeaves.Commands.Create;

public class CreateEntitledLeaveCommand : IRequest<CreatedEntitledLeaveResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid EmployeeId { get; set; }
    public Guid LeaveTypeId { get; set; }
    public DateTime? EntitledDate { get; set; }
    public int? EntitledDays { get; set; }

    public string[] Roles => new[] { Admin, Write, EntitledLeavesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetEntitledLeaves";

    public class CreateEntitledLeaveCommandHandler : IRequestHandler<CreateEntitledLeaveCommand, CreatedEntitledLeaveResponse>
    {
        private readonly IMapper _mapper;
        private readonly IEntitledLeaveRepository _entitledLeaveRepository;
        private readonly EntitledLeaveBusinessRules _entitledLeaveBusinessRules;

        public CreateEntitledLeaveCommandHandler(IMapper mapper, IEntitledLeaveRepository entitledLeaveRepository,
                                         EntitledLeaveBusinessRules entitledLeaveBusinessRules)
        {
            _mapper = mapper;
            _entitledLeaveRepository = entitledLeaveRepository;
            _entitledLeaveBusinessRules = entitledLeaveBusinessRules;
        }

        public async Task<CreatedEntitledLeaveResponse> Handle(CreateEntitledLeaveCommand request, CancellationToken cancellationToken)
        {
            EntitledLeave entitledLeave = _mapper.Map<EntitledLeave>(request);

            await _entitledLeaveRepository.AddAsync(entitledLeave);

            CreatedEntitledLeaveResponse response = _mapper.Map<CreatedEntitledLeaveResponse>(entitledLeave);
            return response;
        }
    }
}
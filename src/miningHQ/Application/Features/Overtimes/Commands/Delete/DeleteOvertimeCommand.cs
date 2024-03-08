using Application.Features.Overtimes.Constants;
using Application.Features.Overtimes.Constants;
using Application.Features.Overtimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Overtimes.Constants.OvertimesOperationClaims;

namespace Application.Features.Overtimes.Commands.Delete;

public class DeleteOvertimeCommand : IRequest<DeletedOvertimeResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, OvertimesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetOvertimes";

    public class DeleteOvertimeCommandHandler : IRequestHandler<DeleteOvertimeCommand, DeletedOvertimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly OvertimeBusinessRules _overtimeBusinessRules;

        public DeleteOvertimeCommandHandler(IMapper mapper, IOvertimeRepository overtimeRepository,
                                         OvertimeBusinessRules overtimeBusinessRules)
        {
            _mapper = mapper;
            _overtimeRepository = overtimeRepository;
            _overtimeBusinessRules = overtimeBusinessRules;
        }

        public async Task<DeletedOvertimeResponse> Handle(DeleteOvertimeCommand request, CancellationToken cancellationToken)
        {
            Overtime? overtime = await _overtimeRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _overtimeBusinessRules.OvertimeShouldExistWhenSelected(overtime);

            await _overtimeRepository.DeleteAsync(overtime!);

            DeletedOvertimeResponse response = _mapper.Map<DeletedOvertimeResponse>(overtime);
            return response;
        }
    }
}
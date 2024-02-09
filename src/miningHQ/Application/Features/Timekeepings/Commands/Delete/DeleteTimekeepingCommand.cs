using Application.Features.Timekeepings.Constants;
using Application.Features.Timekeepings.Constants;
using Application.Features.Timekeepings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Timekeepings.Constants.TimekeepingsOperationClaims;

namespace Application.Features.Timekeepings.Commands.Delete;

public class DeleteTimekeepingCommand : IRequest<DeletedTimekeepingResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, TimekeepingsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTimekeepings";

    public class DeleteTimekeepingCommandHandler : IRequestHandler<DeleteTimekeepingCommand, DeletedTimekeepingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITimekeepingRepository _timekeepingRepository;
        private readonly TimekeepingBusinessRules _timekeepingBusinessRules;

        public DeleteTimekeepingCommandHandler(IMapper mapper, ITimekeepingRepository timekeepingRepository,
                                         TimekeepingBusinessRules timekeepingBusinessRules)
        {
            _mapper = mapper;
            _timekeepingRepository = timekeepingRepository;
            _timekeepingBusinessRules = timekeepingBusinessRules;
        }

        public async Task<DeletedTimekeepingResponse> Handle(DeleteTimekeepingCommand request, CancellationToken cancellationToken)
        {
            Timekeeping? timekeeping = await _timekeepingRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _timekeepingBusinessRules.TimekeepingShouldExistWhenSelected(timekeeping);

            await _timekeepingRepository.DeleteAsync(timekeeping!);

            DeletedTimekeepingResponse response = _mapper.Map<DeletedTimekeepingResponse>(timekeeping);
            return response;
        }
    }
}
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

namespace Application.Features.Timekeepings.Commands.Update;

public class UpdateTimekeepingCommand : IRequest<UpdatedTimekeepingResponse>
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid? EmployeeId { get; set; }
    public bool? Status { get; set; }

    public string[] Roles => new[] { Admin, Write, TimekeepingsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetTimekeepings";

    public class UpdateTimekeepingCommandHandler : IRequestHandler<UpdateTimekeepingCommand, UpdatedTimekeepingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITimekeepingRepository _timekeepingRepository;
        private readonly TimekeepingBusinessRules _timekeepingBusinessRules;

        public UpdateTimekeepingCommandHandler(IMapper mapper, ITimekeepingRepository timekeepingRepository,
                                         TimekeepingBusinessRules timekeepingBusinessRules)
        {
            _mapper = mapper;
            _timekeepingRepository = timekeepingRepository;
            _timekeepingBusinessRules = timekeepingBusinessRules;
        }

        public async Task<UpdatedTimekeepingResponse> Handle(UpdateTimekeepingCommand request, CancellationToken cancellationToken)
        {
            Timekeeping? timekeeping = await _timekeepingRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _timekeepingBusinessRules.TimekeepingShouldExistWhenSelected(timekeeping);
            timekeeping = _mapper.Map(request, timekeeping);

            await _timekeepingRepository.UpdateAsync(timekeeping!);

            UpdatedTimekeepingResponse response = _mapper.Map<UpdatedTimekeepingResponse>(timekeeping);
            return response;
        }
    }
}
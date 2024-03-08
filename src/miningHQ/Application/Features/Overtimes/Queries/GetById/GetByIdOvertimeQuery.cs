using Application.Features.Overtimes.Constants;
using Application.Features.Overtimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Overtimes.Constants.OvertimesOperationClaims;

namespace Application.Features.Overtimes.Queries.GetById;

public class GetByIdOvertimeQuery : IRequest<GetByIdOvertimeResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdOvertimeQueryHandler : IRequestHandler<GetByIdOvertimeQuery, GetByIdOvertimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOvertimeRepository _overtimeRepository;
        private readonly OvertimeBusinessRules _overtimeBusinessRules;

        public GetByIdOvertimeQueryHandler(IMapper mapper, IOvertimeRepository overtimeRepository, OvertimeBusinessRules overtimeBusinessRules)
        {
            _mapper = mapper;
            _overtimeRepository = overtimeRepository;
            _overtimeBusinessRules = overtimeBusinessRules;
        }

        public async Task<GetByIdOvertimeResponse> Handle(GetByIdOvertimeQuery request, CancellationToken cancellationToken)
        {
            Overtime? overtime = await _overtimeRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _overtimeBusinessRules.OvertimeShouldExistWhenSelected(overtime);

            GetByIdOvertimeResponse response = _mapper.Map<GetByIdOvertimeResponse>(overtime);
            return response;
        }
    }
}
using Application.Features.Timekeepings.Constants;
using Application.Features.Timekeepings.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Timekeepings.Constants.TimekeepingsOperationClaims;

namespace Application.Features.Timekeepings.Queries.GetById;

public class GetByIdTimekeepingQuery : IRequest<GetByIdTimekeepingResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdTimekeepingQueryHandler : IRequestHandler<GetByIdTimekeepingQuery, GetByIdTimekeepingResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITimekeepingRepository _timekeepingRepository;
        private readonly TimekeepingBusinessRules _timekeepingBusinessRules;

        public GetByIdTimekeepingQueryHandler(IMapper mapper, ITimekeepingRepository timekeepingRepository, TimekeepingBusinessRules timekeepingBusinessRules)
        {
            _mapper = mapper;
            _timekeepingRepository = timekeepingRepository;
            _timekeepingBusinessRules = timekeepingBusinessRules;
        }

        public async Task<GetByIdTimekeepingResponse> Handle(GetByIdTimekeepingQuery request, CancellationToken cancellationToken)
        {
            Timekeeping? timekeeping = await _timekeepingRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _timekeepingBusinessRules.TimekeepingShouldExistWhenSelected(timekeeping);

            GetByIdTimekeepingResponse response = _mapper.Map<GetByIdTimekeepingResponse>(timekeeping);
            return response;
        }
    }
}
using Application.Features.Quarries.Constants;
using Application.Features.Quarries.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Application.Features.Quarries.Constants.QuarriesOperationClaims;

namespace Application.Features.Quarries.Queries.GetById;

public class GetByIdQuarryQuery : IRequest<GetByIdQuarryResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdQuarryQueryHandler : IRequestHandler<GetByIdQuarryQuery, GetByIdQuarryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryRepository _quarryRepository;
        private readonly QuarryBusinessRules _quarryBusinessRules;

        public GetByIdQuarryQueryHandler(IMapper mapper, IQuarryRepository quarryRepository, QuarryBusinessRules quarryBusinessRules)
        {
            _mapper = mapper;
            _quarryRepository = quarryRepository;
            _quarryBusinessRules = quarryBusinessRules;
        }

        public async Task<GetByIdQuarryResponse> Handle(GetByIdQuarryQuery request, CancellationToken cancellationToken)
        {
            Quarry? quarry = await _quarryRepository.GetAsync(
                predicate: q => q.Id == request.Id,
                include: q => q
                    .Include(q => q.MiningEngineer)
                    .Include(q => q.Employees)
                        .ThenInclude(e => e.Job)
                    .Include(q => q.Employees)
                        .ThenInclude(e => e.Department)
                    .Include(q => q.Machines)
                        .ThenInclude(m => m.MachineType)
                    .Include(q => q.Machines)
                        .ThenInclude(m => m.Model)
                            .ThenInclude(m => m.Brand)
                    .Include(q => q.QuarryFiles)
                    .Include(q => q.QuarryProductions),
                cancellationToken: cancellationToken);
            await _quarryBusinessRules.QuarryShouldExistWhenSelected(quarry);

            GetByIdQuarryResponse response = _mapper.Map<GetByIdQuarryResponse>(quarry);
            return response;
        }
    }
}
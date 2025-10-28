using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.QuarryProductions.Commands.Create;

public class CreateQuarryProductionCommand : IRequest<CreatedQuarryProductionResponse>
{
    public Guid QuarryId { get; set; }
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekEndDate { get; set; }
    public decimal ProductionAmount { get; set; }
    public string? ProductionUnit { get; set; }
    public decimal StockAmount { get; set; }
    public string? StockUnit { get; set; }
    public decimal SalesAmount { get; set; }
    public string? SalesUnit { get; set; }
    public string? Notes { get; set; }

    public class CreateQuarryProductionCommandHandler : IRequestHandler<CreateQuarryProductionCommand, CreatedQuarryProductionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuarryProductionRepository _quarryProductionRepository;

        public CreateQuarryProductionCommandHandler(IMapper mapper, IQuarryProductionRepository quarryProductionRepository)
        {
            _mapper = mapper;
            _quarryProductionRepository = quarryProductionRepository;
        }

        public async Task<CreatedQuarryProductionResponse> Handle(CreateQuarryProductionCommand request, CancellationToken cancellationToken)
        {
            QuarryProduction quarryProduction = _mapper.Map<QuarryProduction>(request);
            quarryProduction.Id = Guid.NewGuid();

            await _quarryProductionRepository.AddAsync(quarryProduction);

            CreatedQuarryProductionResponse response = _mapper.Map<CreatedQuarryProductionResponse>(quarryProduction);
            return response;
        }
    }
}

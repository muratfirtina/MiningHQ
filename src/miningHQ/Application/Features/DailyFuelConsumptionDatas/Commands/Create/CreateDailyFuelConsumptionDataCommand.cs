using Application.Features.DailyFuelConsumptionDatas.Constants;
using Application.Features.DailyFuelConsumptionDatas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.DailyFuelConsumptionDatas.Constants.DailyFuelConsumptionDatasOperationClaims;

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Create;

public class CreateDailyFuelConsumptionDataCommand : IRequest<CreatedDailyFuelConsumptionDataResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public DateTime Date { get; set; }
    public double FuelConsumption { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }

    public string[] Roles => new[] { Admin, Write, DailyFuelConsumptionDatasOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetDailyFuelConsumptionDatas"};

    public class CreateDailyFuelConsumptionDataCommandHandler : IRequestHandler<CreateDailyFuelConsumptionDataCommand, CreatedDailyFuelConsumptionDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
        private readonly DailyFuelConsumptionDataBusinessRules _dailyFuelConsumptionDataBusinessRules;

        public CreateDailyFuelConsumptionDataCommandHandler(IMapper mapper, IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository,
                                         DailyFuelConsumptionDataBusinessRules dailyFuelConsumptionDataBusinessRules)
        {
            _mapper = mapper;
            _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
            _dailyFuelConsumptionDataBusinessRules = dailyFuelConsumptionDataBusinessRules;
        }

        public async Task<CreatedDailyFuelConsumptionDataResponse> Handle(CreateDailyFuelConsumptionDataCommand request, CancellationToken cancellationToken)
        {
            DailyFuelConsumptionData dailyFuelConsumptionData = _mapper.Map<DailyFuelConsumptionData>(request);

            await _dailyFuelConsumptionDataRepository.AddAsync(dailyFuelConsumptionData);

            CreatedDailyFuelConsumptionDataResponse response = _mapper.Map<CreatedDailyFuelConsumptionDataResponse>(dailyFuelConsumptionData);
            return response;
        }
    }
}
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

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Update;

public class UpdateDailyFuelConsumptionDataCommand : IRequest<UpdatedDailyFuelConsumptionDataResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double FuelConsumption { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }

    public string[] Roles => new[] { Admin, Write, DailyFuelConsumptionDatasOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetDailyFuelConsumptionDatas"};

    public class UpdateDailyFuelConsumptionDataCommandHandler : IRequestHandler<UpdateDailyFuelConsumptionDataCommand, UpdatedDailyFuelConsumptionDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
        private readonly DailyFuelConsumptionDataBusinessRules _dailyFuelConsumptionDataBusinessRules;

        public UpdateDailyFuelConsumptionDataCommandHandler(IMapper mapper, IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository,
                                         DailyFuelConsumptionDataBusinessRules dailyFuelConsumptionDataBusinessRules)
        {
            _mapper = mapper;
            _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
            _dailyFuelConsumptionDataBusinessRules = dailyFuelConsumptionDataBusinessRules;
        }

        public async Task<UpdatedDailyFuelConsumptionDataResponse> Handle(UpdateDailyFuelConsumptionDataCommand request, CancellationToken cancellationToken)
        {
            DailyFuelConsumptionData? dailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.GetAsync(predicate: dfcd => dfcd.Id == request.Id, cancellationToken: cancellationToken);
            await _dailyFuelConsumptionDataBusinessRules.DailyFuelConsumptionDataShouldExistWhenSelected(dailyFuelConsumptionData);
            dailyFuelConsumptionData = _mapper.Map(request, dailyFuelConsumptionData);

            await _dailyFuelConsumptionDataRepository.UpdateAsync(dailyFuelConsumptionData!);

            UpdatedDailyFuelConsumptionDataResponse response = _mapper.Map<UpdatedDailyFuelConsumptionDataResponse>(dailyFuelConsumptionData);
            return response;
        }
    }
}
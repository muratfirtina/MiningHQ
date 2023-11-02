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

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Delete;

public class DeleteDailyFuelConsumptionDataCommand : IRequest<DeletedDailyFuelConsumptionDataResponse>//, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, DailyFuelConsumptionDatasOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] {"GetDailyFuelConsumptionDatas"};

    public class DeleteDailyFuelConsumptionDataCommandHandler : IRequestHandler<DeleteDailyFuelConsumptionDataCommand, DeletedDailyFuelConsumptionDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
        private readonly DailyFuelConsumptionDataBusinessRules _dailyFuelConsumptionDataBusinessRules;

        public DeleteDailyFuelConsumptionDataCommandHandler(IMapper mapper, IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository,
                                         DailyFuelConsumptionDataBusinessRules dailyFuelConsumptionDataBusinessRules)
        {
            _mapper = mapper;
            _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
            _dailyFuelConsumptionDataBusinessRules = dailyFuelConsumptionDataBusinessRules;
        }

        public async Task<DeletedDailyFuelConsumptionDataResponse> Handle(DeleteDailyFuelConsumptionDataCommand request, CancellationToken cancellationToken)
        {
            DailyFuelConsumptionData? dailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.GetAsync(predicate: dfcd => dfcd.Id == request.Id, cancellationToken: cancellationToken);
            await _dailyFuelConsumptionDataBusinessRules.DailyFuelConsumptionDataShouldExistWhenSelected(dailyFuelConsumptionData);

            await _dailyFuelConsumptionDataRepository.DeleteAsync(dailyFuelConsumptionData!);

            DeletedDailyFuelConsumptionDataResponse response = _mapper.Map<DeletedDailyFuelConsumptionDataResponse>(dailyFuelConsumptionData);
            return response;
        }
    }
}
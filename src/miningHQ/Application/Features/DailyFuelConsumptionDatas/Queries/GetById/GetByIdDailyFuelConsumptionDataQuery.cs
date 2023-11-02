using Application.Features.DailyFuelConsumptionDatas.Constants;
using Application.Features.DailyFuelConsumptionDatas.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.DailyFuelConsumptionDatas.Constants.DailyFuelConsumptionDatasOperationClaims;

namespace Application.Features.DailyFuelConsumptionDatas.Queries.GetById;

public class GetByIdDailyFuelConsumptionDataQuery : IRequest<GetByIdDailyFuelConsumptionDataResponse>//, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdDailyFuelConsumptionDataQueryHandler : IRequestHandler<GetByIdDailyFuelConsumptionDataQuery, GetByIdDailyFuelConsumptionDataResponse>
    {
        private readonly IMapper _mapper;
        private readonly IDailyFuelConsumptionDataRepository _dailyFuelConsumptionDataRepository;
        private readonly DailyFuelConsumptionDataBusinessRules _dailyFuelConsumptionDataBusinessRules;

        public GetByIdDailyFuelConsumptionDataQueryHandler(IMapper mapper, IDailyFuelConsumptionDataRepository dailyFuelConsumptionDataRepository, DailyFuelConsumptionDataBusinessRules dailyFuelConsumptionDataBusinessRules)
        {
            _mapper = mapper;
            _dailyFuelConsumptionDataRepository = dailyFuelConsumptionDataRepository;
            _dailyFuelConsumptionDataBusinessRules = dailyFuelConsumptionDataBusinessRules;
        }

        public async Task<GetByIdDailyFuelConsumptionDataResponse> Handle(GetByIdDailyFuelConsumptionDataQuery request, CancellationToken cancellationToken)
        {
            DailyFuelConsumptionData? dailyFuelConsumptionData = await _dailyFuelConsumptionDataRepository.GetAsync(predicate: dfcd => dfcd.Id == request.Id, cancellationToken: cancellationToken);
            await _dailyFuelConsumptionDataBusinessRules.DailyFuelConsumptionDataShouldExistWhenSelected(dailyFuelConsumptionData);

            GetByIdDailyFuelConsumptionDataResponse response = _mapper.Map<GetByIdDailyFuelConsumptionDataResponse>(dailyFuelConsumptionData);
            return response;
        }
    }
}
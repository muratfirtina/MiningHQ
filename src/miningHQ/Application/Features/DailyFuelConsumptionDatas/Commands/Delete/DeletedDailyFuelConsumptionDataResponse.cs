using Core.Application.Responses;

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Delete;

public class DeletedDailyFuelConsumptionDataResponse : IResponse
{
    public Guid Id { get; set; }
}
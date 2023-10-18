using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.DailyFuelConsumptionDatas.Commands.Update;

public class UpdatedDailyFuelConsumptionDataResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double FuelConsumption { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
}
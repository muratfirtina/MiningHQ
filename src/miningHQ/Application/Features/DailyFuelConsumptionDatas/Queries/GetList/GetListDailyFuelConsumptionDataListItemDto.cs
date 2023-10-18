using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.DailyFuelConsumptionDatas.Queries.GetList;

public class GetListDailyFuelConsumptionDataListItemDto : IDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public double FuelConsumption { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
}
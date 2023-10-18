using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.DailyWorkDatas.Queries.GetList;

public class GetListDailyWorkDataListItemDto : IDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int WorkingHoursOrKm { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
}
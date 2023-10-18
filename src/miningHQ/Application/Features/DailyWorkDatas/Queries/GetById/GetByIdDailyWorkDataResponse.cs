using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.DailyWorkDatas.Queries.GetById;

public class GetByIdDailyWorkDataResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int WorkingHoursOrKm { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
}
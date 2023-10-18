using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.DailyWorkDatas.Commands.Update;

public class UpdatedDailyWorkDataResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public int WorkingHoursOrKm { get; set; }
    public Guid MachineId { get; set; }
    public Machine Machine { get; set; }
}
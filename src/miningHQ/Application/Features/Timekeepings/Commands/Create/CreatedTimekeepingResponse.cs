using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Timekeepings.Commands.Create;

public class CreatedTimekeepingResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid? EmployeeId { get; set; }
    public TimekeepingStatus Status { get; set; }
}
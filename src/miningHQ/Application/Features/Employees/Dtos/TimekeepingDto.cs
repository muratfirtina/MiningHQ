using Domain.Enums;

namespace Application.Features.Employees.Dtos;

public class TimekeepingDto
{
    public DateTime Date { get; set; }
    public TimekeepingStatus Status { get; set; }
}
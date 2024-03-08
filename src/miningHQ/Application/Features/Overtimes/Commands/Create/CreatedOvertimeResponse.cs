using Core.Application.Responses;

namespace Application.Features.Overtimes.Commands.Create;

public class CreatedOvertimeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }
}
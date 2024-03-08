using Core.Application.Responses;

namespace Application.Features.Overtimes.Commands.Update;

public class UpdatedOvertimeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.Overtimes.Queries.GetById;

public class GetByIdOvertimeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }
}
using Core.Application.Dtos;

namespace Application.Features.Overtimes.Queries.GetListByDynamic;

public class GetListByDynamicOvertimeListItemDto:IDto
{
    public string Id { get; set; }
    public string? EmployeeId { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }
    public float? TotalOvertimeHours { get; set; }
}
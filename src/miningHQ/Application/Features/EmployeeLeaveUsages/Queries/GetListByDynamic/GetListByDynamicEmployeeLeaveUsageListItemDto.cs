using Core.Application.Dtos;

namespace Application.Features.EmployeeLeaveUsages.Queries.GetListByDynamic;

public class GetListByDynamicEmployeeLeaveUsageListItemDto: IDto
{
    
    public string Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? LeaveTypeId { get; set; }
    public string? LeaveTypeName { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int? UsedDays { get; set; }
}
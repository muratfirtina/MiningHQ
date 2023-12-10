using Core.Application.Dtos;

namespace Application.Features.EntitledLeaves.Dtos;

public class EmployeeEntitledLeaveDto: IDto
{
    public string Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? LeaveTypeId { get; set; }
    public string? LeaveTypeName { get; set; }
    public DateTime? EntitledDate { get; set; }
    public int? EntitledDays { get; set; }
}
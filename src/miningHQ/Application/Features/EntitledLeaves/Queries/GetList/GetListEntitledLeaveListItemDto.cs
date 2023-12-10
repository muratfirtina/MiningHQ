using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.EntitledLeaves.Queries.GetList;

public class GetListEntitledLeaveListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public Guid LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public DateTime? EntitledDate { get; set; }
}
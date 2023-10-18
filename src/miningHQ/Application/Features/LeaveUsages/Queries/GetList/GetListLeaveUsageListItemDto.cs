using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.LeaveUsages.Queries.GetList;

public class GetListLeaveUsageListItemDto : IDto
{
    
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; }
    public EmployeeLeave EmployeeLeave { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
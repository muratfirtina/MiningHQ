using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.EmployeeLeaveUsages.Queries.GetList;

public class GetListEmployeeLeaveUsageListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LeaveTypeName { get; set; }
    public int UsedDays { get; set; }
}
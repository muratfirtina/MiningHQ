using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.EmployeeLeaveUsages.Queries.GetList;

public class GetListEmployeeLeaveUsageListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; }
}
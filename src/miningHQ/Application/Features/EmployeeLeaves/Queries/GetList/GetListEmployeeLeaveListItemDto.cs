using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.EmployeeLeaves.Queries.GetList;

public class GetListEmployeeLeaveListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; }
}
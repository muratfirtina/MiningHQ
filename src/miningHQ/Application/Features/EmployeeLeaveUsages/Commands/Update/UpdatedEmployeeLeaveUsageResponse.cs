using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.EmployeeLeaveUsages.Commands.Update;

public class UpdatedEmployeeLeaveUsageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; }
}
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.EmployeeLeaveUsages.Commands.Create;

public class CreatedEmployeeLeaveUsageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public string LeaveTypeName { get; set; }
    public int UsedDays { get; set; }
}
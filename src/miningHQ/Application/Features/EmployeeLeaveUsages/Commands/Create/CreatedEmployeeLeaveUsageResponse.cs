using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.EmployeeLeaveUsages.Commands.Create;

public class CreatedEmployeeLeaveUsageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string LeaveTypeName { get; set; }
    public DateTime UsageDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public int UsedDays { get; set; }
}
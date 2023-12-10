using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.LeaveTypes.Commands.Create;

public class CreatedLeaveTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; }
    public EmployeeLeaveUsage EmployeeLeaveUsage { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
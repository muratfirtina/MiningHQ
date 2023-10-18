using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.LeaveUsages.Commands.Update;

public class UpdatedLeaveUsageResponse : IResponse
{
    
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; }
    public EmployeeLeave EmployeeLeave { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
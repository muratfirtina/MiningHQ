using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.LeaveUsages.Queries.GetById;

public class GetByIdLeaveUsageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeLeaveId { get; set; }
    public EmployeeLeave EmployeeLeave { get; set; }
    public DateTime? UsageDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.EmployeeLeaveUsages.Queries.GetById;

public class GetByIdEmployeeLeaveUsageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; }
}
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.EmployeeLeaves.Queries.GetById;

public class GetByIdEmployeeLeaveResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; }
    public int TotalLeaveDays { get; set; }
}
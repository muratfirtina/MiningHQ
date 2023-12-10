using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.EntitledLeaves.Queries.GetById;

public class GetByIdEntitledLeaveResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public Guid LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public DateTime? EntitledDate { get; set; }
}
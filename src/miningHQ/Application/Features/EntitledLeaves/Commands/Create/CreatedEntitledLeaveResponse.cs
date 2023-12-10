using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.EntitledLeaves.Commands.Create;

public class CreatedEntitledLeaveResponse : IResponse
{
    public string Id { get; set; }
    public string? EmployeeId { get; set; }
    public string? LeaveTypeId { get; set; }
    public DateTime? EntitledDate { get; set; }
    public int? EntitledDays { get; set; }
}
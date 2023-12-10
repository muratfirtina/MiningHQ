using Application.Features.EntitledLeaves.Dtos;

namespace Application.Features.EntitledLeaves.Queries.GetByEmployeeId;

public class GetEntitledLeavesByEmployeeIdResponse
{
    public List<EmployeeEntitledLeaveDto> EntitledLeaves { get; set; }
}
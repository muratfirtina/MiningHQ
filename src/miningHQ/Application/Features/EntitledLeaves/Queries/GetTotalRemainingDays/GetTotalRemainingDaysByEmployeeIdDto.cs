using Core.Application.Dtos;

namespace Application.Features.EntitledLeaves.Queries.GetTotalRemainingDays;

public class GetTotalRemainingDaysByEmployeeIdDto : IDto
{
    public string? EmployeeId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? TotalRemainingDays { get; set; }
}
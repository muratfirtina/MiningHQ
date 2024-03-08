using Core.Application.Dtos;

namespace Application.Features.Overtimes.Queries.GetList;

public class GetListOvertimeListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? OvertimeDate { get; set; }
    public float? OvertimeHours { get; set; }
}
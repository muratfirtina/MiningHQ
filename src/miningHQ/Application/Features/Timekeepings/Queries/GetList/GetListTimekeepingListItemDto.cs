using Application.Features.Employees.Dtos;
using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Timekeepings.Queries.GetList;

public class GetListTimekeepingListItemDto : IDto
{
    
    public Guid? EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? HireDate { get; set; } 
    public int? TotalRemainingDays { get; set; }
    
    public ICollection<TimekeepingDto>? Timekeepings { get; set; }
    
}
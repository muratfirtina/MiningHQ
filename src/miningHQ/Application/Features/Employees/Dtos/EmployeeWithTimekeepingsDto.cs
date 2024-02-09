using Core.Application.Dtos;

namespace Application.Features.Employees.Dtos;

public class EmployeeWithTimekeepingsDto: IDto
{
    public Guid EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<TimekeepingDto> Timekeepings { get; set; }
}
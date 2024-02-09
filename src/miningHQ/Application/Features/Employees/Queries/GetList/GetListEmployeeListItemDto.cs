using Application.Features.Employees.Dtos;
using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Employees.Queries.GetList;

public class GetListEmployeeListItemDto : IDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobName { get; set; }
    public string QuarryName { get; set; }
    public string? Address { get; set; }
    public string Phone { get; set; }
    public string LicenseType { get; set; }
    public string? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public ICollection<EmployeeFile>? EmployeeImageFiles { get; set; }
    public ICollection<TimekeepingDto>? Timekeepings { get; set; }
}

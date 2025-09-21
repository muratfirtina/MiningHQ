using Core.Application.Dtos;

namespace Application.Features.Employees.Queries.GetList.ShortDetail;

public class GetListByEmplooyeeShortDetailItemDto : IDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobName { get; set; }
    public string DepartmentName { get; set; }
    public string QuarryName { get; set; }
    public string? Address { get; set; }
    public string Phone { get; set; }
    public string LicenseType { get; set; }
    public string? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
    public DateTime? BirthDate { get; set; }
    public DateTime? HireDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    
}
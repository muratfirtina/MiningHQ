using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Employees.Queries.GetById;

public class GetByIdEmployeeResponse : IResponse
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? DepartmentName { get; set; }
    public string? JobName { get; set; }
    public string? QuarryName { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? HireDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public LicenseTypes? LicenseType { get; set; }
    public OperatorLicense? OperatorLicense { get; set; }
    public TypeOfBlood? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
    public int? TotalUsedLeaveDays { get; set; }
    public int? TotalEntitledLeaveDays { get; set; }
    
    public int? CurrentLeaveDays { get; set; }
    public ICollection<EmployeeFile>? EmployeeFiles { get; set; }
    

}
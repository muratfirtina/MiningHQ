using Core.Application.Responses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.Employees.Commands.Update;

public class UpdatedEmployeeResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? DepartmentId { get; set; }
    public string? JobId { get; set; }
    public string? QuarryId { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? HireDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public LicenseTypes? LicenseType { get; set; }
    public OperatorLicense? OperatorLicense { get; set; }
    public TypeOfBlood? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
}
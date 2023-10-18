using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Employees.Queries.GetById;

public class GetByIdEmployeeResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public Guid? JobId { get; set; }
    public Job? Job { get; set; }
    public Guid? QuarryId { get; set; }
    public Quarry? Quarry { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? HireDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public string? LicenseType { get; set; }
    public string? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
}
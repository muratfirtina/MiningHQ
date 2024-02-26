using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Employee:Entity<Guid>
{
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
    public TypeOfBlood TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
    public ICollection<Machine>? Machines { get; set; } = new List<Machine>();
    public ICollection<EmployeeFile>? EmployeeFiles { get; set; } = new List<EmployeeFile>();
    public ICollection<EmployeeLeaveUsage>? EmployeeLeaveUsages { get; set; }
    public ICollection<EntitledLeave>? EntitledLeaves { get; set; }
    public ICollection<Timekeeping>? Timekeepings { get; set; }
    

    public Employee()
    {
        
    }

    public Employee(Guid id, string firstName, string lastName):this()
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
    
   
}
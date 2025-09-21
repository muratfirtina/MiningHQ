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
    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public DateTime? HireDate { get; set; }
    public DateTime? DepartureDate { get; set; }
    public LicenseTypes? LicenseType { get; set; }
    public OperatorLicense? OperatorLicense { get; set; }
    public TypeOfBlood? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
    public ICollection<Machine>? Machines { get; set; }
    public ICollection<EmployeeFile>? EmployeeFiles { get; set; }
    public Guid? EmployeePhotoId { get; set; }
    public EmployeePhoto? EmployeePhoto { get; set; }
    public ICollection<EmployeeLeaveUsage>? EmployeeLeaveUsages { get; set; }
    public ICollection<EntitledLeave>? EntitledLeaves { get; set; }
    public ICollection<Timekeeping>? Timekeepings { get; set; }
    
    public ICollection<Overtime>? Overtimes { get; set; }
    

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
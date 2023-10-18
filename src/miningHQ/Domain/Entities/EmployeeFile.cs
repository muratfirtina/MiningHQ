namespace Domain.Entities;

public class EmployeeFile:File
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
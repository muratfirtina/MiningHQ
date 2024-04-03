namespace Domain.Entities;

public class EmployeePhoto:File
{
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
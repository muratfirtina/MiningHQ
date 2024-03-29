namespace Domain.Entities;

public class EmployeeFile:File
{
    public bool Showcase { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
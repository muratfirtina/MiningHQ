namespace Domain.Entities;

public class EmployeeFile:File
{
    public bool Showcase { get; set; }
    ICollection<Employee> Employees { get; set; }
}
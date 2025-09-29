namespace Domain.Entities;

public class MachineFile : File
{
    public bool Showcase { get; set; }
    public ICollection<Machine> Machines { get; set; } = new List<Machine>();
}

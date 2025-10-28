namespace Domain.Entities;

public class QuarryFile : File
{
    public bool Showcase { get; set; }
    public ICollection<Quarry> Quarries { get; set; }
    
    public QuarryFile()
    {
        
    }
}

using Core.Persistence.Repositories;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class File:Entity<Guid>
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Path { get; set; }
    [NotMapped]
    public string Url { get; set; }
    public string Storage { get; set; }

    [NotMapped]
    public override DateTime? UpdatedDate { get =>base.UpdatedDate; set=>base.UpdatedDate=value; } 
}
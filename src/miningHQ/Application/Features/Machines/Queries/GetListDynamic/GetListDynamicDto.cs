namespace Application.Features.Machines.Queries.GetListDynamic;

public class GetListDynamicDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName { get; set; }
    public string? MachineTypeName { get; set; }
    public string? QuarryName { get; set; }
    public string? SerialNumber { get; set; }
   
}
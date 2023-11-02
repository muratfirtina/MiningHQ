using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Machines.Dtos;
public class MachineDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SerialNumber { get; set; }
    public string ModelId { get; set; }
    public string ModelName { get; set; }
    public string QuarryId { get; set; }
    public string QuarryName { get; set; }
    public string MachineTypeId { get; set; }
    public string MachineTypeName { get; set; }
    public string BrandId { get; set; }
    public string BrandName { get; set; }
    
}
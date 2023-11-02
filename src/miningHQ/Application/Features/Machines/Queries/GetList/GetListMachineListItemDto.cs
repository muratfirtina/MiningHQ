using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Machines.Queries.GetList;

public class GetListMachineListItemDto : IDto
{
    public string Id { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public string QuarryName { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string MachineTypeName { get; set; }
}
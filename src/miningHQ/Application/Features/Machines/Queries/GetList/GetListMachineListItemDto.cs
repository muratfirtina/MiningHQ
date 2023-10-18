using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Machines.Queries.GetList;

public class GetListMachineListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public Model? Model { get; set; }
    public Guid QuarryId { get; set; }
    public Quarry? Quarry { get; set; }
    public string SerialNumber { get; set; }
    public string? Name { get; set; }
    public Guid MachineTypeId { get; set; }
    public MachineType MachineType { get; set; }
}
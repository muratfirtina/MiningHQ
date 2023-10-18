using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Machines.Commands.Update;

public class UpdatedMachineResponse : IResponse
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
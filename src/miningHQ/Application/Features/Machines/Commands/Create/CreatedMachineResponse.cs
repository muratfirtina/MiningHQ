using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Machines.Commands.Create;

public class CreatedMachineResponse : IResponse
{
    public string Id { get; set; }
    public string? ModelId { get; set; }
    public string? QuarryId { get; set; }
    public string SerialNumber { get; set; }
    public string Name { get; set; }
    public string? MachineTypeId { get; set; }
    
}
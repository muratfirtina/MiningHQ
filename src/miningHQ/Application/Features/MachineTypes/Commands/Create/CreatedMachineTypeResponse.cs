using Core.Application.Responses;

namespace Application.Features.MachineTypes.Commands.Create;

public class CreatedMachineTypeResponse : IResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
}
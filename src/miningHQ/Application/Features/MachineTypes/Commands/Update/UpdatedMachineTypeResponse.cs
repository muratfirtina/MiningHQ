using Core.Application.Responses;

namespace Application.Features.MachineTypes.Commands.Update;

public class UpdatedMachineTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
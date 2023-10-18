using Core.Application.Responses;

namespace Application.Features.MachineTypes.Commands.Delete;

public class DeletedMachineTypeResponse : IResponse
{
    public Guid Id { get; set; }
}
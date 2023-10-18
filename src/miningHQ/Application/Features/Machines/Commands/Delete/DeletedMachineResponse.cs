using Core.Application.Responses;

namespace Application.Features.Machines.Commands.Delete;

public class DeletedMachineResponse : IResponse
{
    public Guid Id { get; set; }
}
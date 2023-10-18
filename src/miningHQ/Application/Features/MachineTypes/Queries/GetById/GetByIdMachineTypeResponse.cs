using Core.Application.Responses;

namespace Application.Features.MachineTypes.Queries.GetById;

public class GetByIdMachineTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
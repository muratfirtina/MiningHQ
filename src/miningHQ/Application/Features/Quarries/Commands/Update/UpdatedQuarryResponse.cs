using Core.Application.Responses;

namespace Application.Features.Quarries.Commands.Update;

public class UpdatedQuarryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
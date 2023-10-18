using Core.Application.Responses;

namespace Application.Features.Quarries.Commands.Create;

public class CreatedQuarryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
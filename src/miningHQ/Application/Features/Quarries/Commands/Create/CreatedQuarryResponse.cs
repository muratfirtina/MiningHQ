using Core.Application.Responses;

namespace Application.Features.Quarries.Commands.Create;

public class CreatedQuarryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Location { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? CoordinateDescription { get; set; }
    public Guid? MiningEngineerId { get; set; }
}
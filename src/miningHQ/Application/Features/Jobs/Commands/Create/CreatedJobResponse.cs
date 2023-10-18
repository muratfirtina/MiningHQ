using Core.Application.Responses;

namespace Application.Features.Jobs.Commands.Create;

public class CreatedJobResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
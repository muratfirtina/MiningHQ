using Core.Application.Responses;

namespace Application.Features.Files.Commands.Update;

public class UpdatedFileResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
}
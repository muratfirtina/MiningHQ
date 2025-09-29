using Core.Application.Responses;

namespace Application.Features.Machines.Queries.GetFilesByMachineId;

public class GetMachineFilesDto : IResponse
{
    public string? Category { get; set; }
    public string? Path { get; set; }
    public string? FileName { get; set; }
    public string? Url { get; set; }
    public Guid Id { get; set; }
    public string? Storage { get; set; }
    public bool Showcase { get; set; }
}

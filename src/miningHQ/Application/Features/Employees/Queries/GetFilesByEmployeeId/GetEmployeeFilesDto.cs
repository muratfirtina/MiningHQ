using Core.Application.Responses;

namespace Application.Features.Employees.Queries.GetFilesByEmployeeId;

public class GetEmployeeFilesDto : IResponse
{
    public string? FileName { get; set; }
    public string? Category { get; set; }
    public string? Path { get; set; }
    public string? Id { get; set; }
    public string? Storage { get; set; }
    
    public bool Showcase { get; set; }
}
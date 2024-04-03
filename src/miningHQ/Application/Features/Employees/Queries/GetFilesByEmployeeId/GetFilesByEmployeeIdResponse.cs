using Core.Application.Responses;

namespace Application.Features.Employees.Queries.GetFilesByEmployeeId;

public class GetFilesByEmployeeIdResponse : IResponse
{
    public string? FileName { get; set; }
    public string? Path { get; set; }
    public string? Id { get; set; }
    
    public bool Showcase { get; set; }
}
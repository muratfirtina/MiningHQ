// Application/Features/Employees/Queries/GetEmployeePhotoBase64/GetEmployeePhotoBase64Response.cs
using Core.Application.Responses;

namespace Application.Features.Employees.Queries.GetEmployeePhotoBase64;

public class GetEmployeePhotoBase64Response : IResponse
{
    public string Id { get; set; }
    public string EmployeeId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
    public string Url { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    
    // ⭐ Base64 için ek alanlar
    public string Base64 { get; set; }
    public string MimeType { get; set; }
    public long FileSize { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
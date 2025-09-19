using Core.Application.Responses;

namespace Application.Features.Employees.Queries.GetEmployeePhoto;

public class GetEmployeePhotoResponse : IResponse
{
    public string Id { get; set; }
    public string EmployeeId { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
    public string Url { get; set; } // ⭐ URL field'ı
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
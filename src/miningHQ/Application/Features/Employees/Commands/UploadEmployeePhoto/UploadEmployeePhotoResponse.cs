using Core.Application.Responses;

namespace Application.Features.Employees.Commands.UploadEmployeePhoto;

public class UploadEmployeePhotoResponse: IResponse
{
    public string Id { get; set; }
    public string EmployeeId { get; set; }
}
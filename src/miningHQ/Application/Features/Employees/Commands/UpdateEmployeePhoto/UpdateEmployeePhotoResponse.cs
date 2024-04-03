using Core.Application.Responses;

namespace Application.Features.Employees.Commands.UpdateEmployeePhoto;

public class UpdateEmployeePhotoResponse: IResponse
{
    public string Id { get; set; }
    public string EmployeeId { get; set; }
}
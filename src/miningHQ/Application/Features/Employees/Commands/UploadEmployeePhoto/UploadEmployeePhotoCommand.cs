using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Commands.UploadEmployeePhoto;

public class UploadEmployeePhotoCommand: IRequest<UploadEmployeePhotoResponse>
{
    public string EmployeeId { get; set; }
    
}

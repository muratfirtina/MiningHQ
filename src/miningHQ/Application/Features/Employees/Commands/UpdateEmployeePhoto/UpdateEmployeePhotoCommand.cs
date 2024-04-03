using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Employees.Commands.UpdateEmployeePhoto;

public class UpdateEmployeePhotoCommand: IRequest<UpdateEmployeePhotoResponse>
{
    public string EmployeeId { get; set; }
    
}

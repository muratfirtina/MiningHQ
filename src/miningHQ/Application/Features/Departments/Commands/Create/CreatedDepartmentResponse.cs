using Core.Application.Responses;

namespace Application.Features.Departments.Commands.Create;

public class CreatedDepartmentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
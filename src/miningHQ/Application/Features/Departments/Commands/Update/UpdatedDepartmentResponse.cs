using Core.Application.Responses;

namespace Application.Features.Departments.Commands.Update;

public class UpdatedDepartmentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
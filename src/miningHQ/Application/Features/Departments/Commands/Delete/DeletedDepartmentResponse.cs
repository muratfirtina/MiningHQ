using Core.Application.Responses;

namespace Application.Features.Departments.Commands.Delete;

public class DeletedDepartmentResponse : IResponse
{
    public Guid Id { get; set; }
}
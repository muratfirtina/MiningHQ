using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Departments.Queries.GetById;

public class GetByIdDepartmentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<Job>? Jobs { get; set; }
}
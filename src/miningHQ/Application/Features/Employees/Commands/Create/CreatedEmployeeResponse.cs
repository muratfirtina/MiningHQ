using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Employees.Commands.Create;

public class CreatedEmployeeResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobName { get; set; }
}
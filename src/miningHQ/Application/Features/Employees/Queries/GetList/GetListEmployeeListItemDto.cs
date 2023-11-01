using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Employees.Queries.GetList;

public class GetListEmployeeListItemDto : IDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobName { get; set; }
    public string QuarryName { get; set; }
}
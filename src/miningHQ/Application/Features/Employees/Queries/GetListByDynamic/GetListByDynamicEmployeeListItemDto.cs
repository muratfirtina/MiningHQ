using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Employees.Queries.GetListByDynamic;

public class GetListByDynamicEmployeeListItemDto: IDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? JobName { get; set; }
    public string? QuarryName { get; set; }
    public string? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
 
    
}
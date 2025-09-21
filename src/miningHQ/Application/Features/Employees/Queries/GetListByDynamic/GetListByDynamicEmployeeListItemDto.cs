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
    public LicenseTypes? LicenseType { get; set; }
    public OperatorLicense? OperatorLicense { get; set; }
    public TypeOfBlood? TypeOfBlood { get; set; }
    public string? EmergencyContact { get; set; }
 
    
}
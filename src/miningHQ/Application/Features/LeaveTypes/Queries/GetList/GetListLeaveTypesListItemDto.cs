using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.LeaveTypes.Queries.GetList;

public class GetListLeaveTypesListItemDto : IDto
{
    
    public string? Id { get; set; }
    public string? Name { get; set; }
    
}
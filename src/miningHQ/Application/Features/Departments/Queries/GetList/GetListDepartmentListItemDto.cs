using Core.Application.Dtos;

namespace Application.Features.Departments.Queries.GetList;

public class GetListDepartmentListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
using Core.Application.Dtos;

namespace Application.Features.Jobs.Queries.GetList;

public class GetListJobListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
}
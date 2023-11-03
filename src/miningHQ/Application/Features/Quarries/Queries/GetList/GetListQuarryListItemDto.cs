using Core.Application.Dtos;

namespace Application.Features.Quarries.Queries.GetList;

public class GetListQuarryListItemDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}
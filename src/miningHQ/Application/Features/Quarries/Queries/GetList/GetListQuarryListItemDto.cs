using Core.Application.Dtos;

namespace Application.Features.Quarries.Queries.GetList;

public class GetListQuarryListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
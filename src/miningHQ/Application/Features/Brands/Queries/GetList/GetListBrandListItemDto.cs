using Core.Application.Dtos;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandListItemDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}
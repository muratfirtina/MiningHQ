using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelListItemDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string BrandName { get; set; }
}
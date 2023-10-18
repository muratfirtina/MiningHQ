using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Models.Queries.GetList;

public class GetListModelListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public Brand? Brand { get; set; }
}
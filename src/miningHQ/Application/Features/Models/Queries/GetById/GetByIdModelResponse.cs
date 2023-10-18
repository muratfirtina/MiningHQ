using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Models.Queries.GetById;

public class GetByIdModelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public Brand? Brand { get; set; }
}
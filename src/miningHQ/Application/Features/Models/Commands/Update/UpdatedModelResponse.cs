using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Models.Commands.Update;

public class UpdatedModelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public Brand? Brand { get; set; }
}
using Core.Application.Responses;
using Domain.Entities;

namespace Application.Features.Models.Commands.Create;

public class CreatedModelResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BrandName{ get; set; }
}
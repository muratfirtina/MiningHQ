using Core.Application.Responses;

namespace Application.Features.Quarries.Queries.GetById;

public class GetByIdQuarryResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
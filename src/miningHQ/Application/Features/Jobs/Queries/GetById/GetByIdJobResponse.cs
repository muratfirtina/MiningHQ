using Core.Application.Responses;

namespace Application.Features.Jobs.Queries.GetById;

public class GetByIdJobResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
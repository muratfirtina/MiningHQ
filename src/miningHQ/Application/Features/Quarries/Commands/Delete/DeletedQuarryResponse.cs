using Core.Application.Responses;

namespace Application.Features.Quarries.Commands.Delete;

public class DeletedQuarryResponse : IResponse
{
    public Guid Id { get; set; }
}
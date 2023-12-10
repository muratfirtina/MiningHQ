using Core.Application.Responses;

namespace Application.Features.EntitledLeaves.Commands.Delete;

public class DeletedEntitledLeaveResponse : IResponse
{
    public Guid Id { get; set; }
}
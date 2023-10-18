using Core.Application.Responses;

namespace Application.Features.Jobs.Commands.Delete;

public class DeletedJobResponse : IResponse
{
    public Guid Id { get; set; }
}
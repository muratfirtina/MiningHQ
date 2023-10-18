using Core.Application.Responses;

namespace Application.Features.Files.Commands.Delete;

public class DeletedFileResponse : IResponse
{
    public Guid Id { get; set; }
}
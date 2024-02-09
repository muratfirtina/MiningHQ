using Core.Application.Responses;

namespace Application.Features.Timekeepings.Commands.Delete;

public class DeletedTimekeepingResponse : IResponse
{
    public Guid Id { get; set; }
}
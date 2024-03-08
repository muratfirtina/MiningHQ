using Core.Application.Responses;

namespace Application.Features.Overtimes.Commands.Delete;

public class DeletedOvertimeResponse : IResponse
{
    public Guid Id { get; set; }
}
using Core.Application.Responses;

namespace Application.Features.LeaveUsages.Commands.Delete;

public class DeletedLeaveUsageResponse : IResponse
{
    public Guid Id { get; set; }
}
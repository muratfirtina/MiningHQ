using Core.Application.Responses;

namespace Application.Features.LeaveTypes.Commands.Delete;

public class DeletedLeaveTypeResponse : IResponse
{
    public Guid Id { get; set; }
}
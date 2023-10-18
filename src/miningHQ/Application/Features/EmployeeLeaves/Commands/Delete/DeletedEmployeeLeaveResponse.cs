using Core.Application.Responses;

namespace Application.Features.EmployeeLeaves.Commands.Delete;

public class DeletedEmployeeLeaveResponse : IResponse
{
    public Guid Id { get; set; }
}
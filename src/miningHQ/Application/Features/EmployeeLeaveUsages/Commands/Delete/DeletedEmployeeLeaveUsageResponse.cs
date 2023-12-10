using Core.Application.Responses;

namespace Application.Features.EmployeeLeaveUsages.Commands.Delete;

public class DeletedEmployeeLeaveUsageResponse : IResponse
{
    public Guid Id { get; set; }
}
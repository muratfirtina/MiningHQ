using Core.Application.Responses;

namespace Application.Features.Maintenances.Commands.Delete;

public class DeletedMaintenanceResponse : IResponse
{
    public Guid Id { get; set; }
}
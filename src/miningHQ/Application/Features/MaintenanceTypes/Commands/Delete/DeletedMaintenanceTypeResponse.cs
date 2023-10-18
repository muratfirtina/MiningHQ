using Core.Application.Responses;

namespace Application.Features.MaintenanceTypes.Commands.Delete;

public class DeletedMaintenanceTypeResponse : IResponse
{
    public Guid Id { get; set; }
}
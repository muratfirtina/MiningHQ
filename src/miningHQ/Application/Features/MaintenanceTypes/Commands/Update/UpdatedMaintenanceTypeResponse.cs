using Core.Application.Responses;

namespace Application.Features.MaintenanceTypes.Commands.Update;

public class UpdatedMaintenanceTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
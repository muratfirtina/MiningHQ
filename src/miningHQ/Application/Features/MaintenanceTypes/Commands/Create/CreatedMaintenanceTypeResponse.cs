using Core.Application.Responses;

namespace Application.Features.MaintenanceTypes.Commands.Create;

public class CreatedMaintenanceTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
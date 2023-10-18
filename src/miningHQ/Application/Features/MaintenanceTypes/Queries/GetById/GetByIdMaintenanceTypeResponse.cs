using Core.Application.Responses;

namespace Application.Features.MaintenanceTypes.Queries.GetById;

public class GetByIdMaintenanceTypeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
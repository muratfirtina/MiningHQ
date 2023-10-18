using Core.Application.Dtos;

namespace Application.Features.MaintenanceTypes.Queries.GetList;

public class GetListMaintenanceTypeListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
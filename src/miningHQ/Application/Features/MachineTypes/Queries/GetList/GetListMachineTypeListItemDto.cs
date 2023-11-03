using Core.Application.Dtos;

namespace Application.Features.MachineTypes.Queries.GetList;

public class GetListMachineTypeListItemDto : IDto
{
    public string Id { get; set; }
    public string Name { get; set; }
}
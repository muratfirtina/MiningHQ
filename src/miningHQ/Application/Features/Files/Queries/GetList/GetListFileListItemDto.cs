using Core.Application.Dtos;

namespace Application.Features.Files.Queries.GetList;

public class GetListFileListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
}
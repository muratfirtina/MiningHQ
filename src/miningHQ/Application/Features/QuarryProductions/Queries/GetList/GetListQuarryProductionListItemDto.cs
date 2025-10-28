using Core.Application.Dtos;

namespace Application.Features.QuarryProductions.Queries.GetList;

public class GetListQuarryProductionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid QuarryId { get; set; }
    public DateTime WeekStartDate { get; set; }
    public DateTime WeekEndDate { get; set; }
    public decimal ProductionAmount { get; set; }
    public string? ProductionUnit { get; set; }
    public decimal StockAmount { get; set; }
    public string? StockUnit { get; set; }
    public decimal SalesAmount { get; set; }
    public string? SalesUnit { get; set; }
    public string? Notes { get; set; }
}

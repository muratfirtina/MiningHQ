using Core.Application.Responses;

namespace Application.Features.DailyEntries.Commands.BulkCreateDailyEntry;

public class BulkCreateDailyEntryResponse : IResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public int SuccessCount { get; set; }
    public int ErrorCount { get; set; }
    public List<string> ErrorMessages { get; set; }
    public DateTime EntryDate { get; set; }
}

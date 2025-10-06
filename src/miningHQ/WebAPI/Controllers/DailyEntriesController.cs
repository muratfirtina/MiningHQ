using Application.Features.DailyEntries.Commands.BulkCreateDailyEntry;
using Application.Features.DailyEntries.Queries.GetMachinesForDailyEntry;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DailyEntriesController : BaseController
{
    [HttpGet("machines")]
    public async Task<IActionResult> GetMachinesForDailyEntry()
    {
        GetMachinesForDailyEntryQuery query = new();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> BulkCreateDailyEntry([FromBody] BulkCreateDailyEntryCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
}

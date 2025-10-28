using Application.Features.QuarryProductions.Commands.Create;
using Application.Features.QuarryProductions.Queries.GetById;
using Application.Features.QuarryProductions.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuarryProductionsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateQuarryProductionCommand createQuarryProductionCommand)
    {
        CreatedQuarryProductionResponse response = await Mediator.Send(createQuarryProductionCommand);
        return Created(uri: "", response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQuarryProductionQuery getListQuarryProductionQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListQuarryProductionListItemDto> response = await Mediator.Send(getListQuarryProductionQuery);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdQuarryProductionQuery getByIdQuarryProductionQuery = new() { Id = id };
        GetByIdQuarryProductionResponse response = await Mediator.Send(getByIdQuarryProductionQuery);
        return Ok(response);
    }
}

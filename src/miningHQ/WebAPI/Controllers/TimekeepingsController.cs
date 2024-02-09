using Application.Features.Timekeepings.Commands.Create;
using Application.Features.Timekeepings.Commands.Delete;
using Application.Features.Timekeepings.Commands.Update;
using Application.Features.Timekeepings.Queries.GetById;
using Application.Features.Timekeepings.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimekeepingsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTimekeepingCommand createTimekeepingCommand)
    {
        CreatedTimekeepingResponse response = await Mediator.Send(createTimekeepingCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTimekeepingCommand updateTimekeepingCommand)
    {
        UpdatedTimekeepingResponse response = await Mediator.Send(updateTimekeepingCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTimekeepingResponse response = await Mediator.Send(new DeleteTimekeepingCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTimekeepingResponse response = await Mediator.Send(new GetByIdTimekeepingQuery { Id = id });
        return Ok(response);
    }

    [HttpGet("{year:int}/{month:int}")]
    public async Task<IActionResult> GetList([FromRoute] int year, [FromRoute] int month, [FromQuery] PageRequest pageRequest)
    {
        GetListTimekeepingQuery getListTimekeepingQuery = new() { PageRequest = pageRequest, Year = year, Month = month };
        GetListResponse<GetListTimekeepingListItemDto> response = await Mediator.Send(getListTimekeepingQuery);
        return Ok(response);
    }
}
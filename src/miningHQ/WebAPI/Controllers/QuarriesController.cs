using Application.Features.Quarries.Commands.Create;
using Application.Features.Quarries.Commands.Delete;
using Application.Features.Quarries.Commands.Update;
using Application.Features.Quarries.Queries.GetById;
using Application.Features.Quarries.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuarriesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateQuarryCommand createQuarryCommand)
    {
        CreatedQuarryResponse response = await Mediator.Send(createQuarryCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateQuarryCommand updateQuarryCommand)
    {
        UpdatedQuarryResponse response = await Mediator.Send(updateQuarryCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedQuarryResponse response = await Mediator.Send(new DeleteQuarryCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdQuarryResponse response = await Mediator.Send(new GetByIdQuarryQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQuarryQuery getListQuarryQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListQuarryListItemDto> response = await Mediator.Send(getListQuarryQuery);
        return Ok(response);
    }
}
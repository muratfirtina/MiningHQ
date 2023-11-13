using Application.Features.Machines.Commands.Create;
using Application.Features.Machines.Commands.Delete;
using Application.Features.Machines.Commands.Update;
using Application.Features.Machines.Queries.GetById;
using Application.Features.Machines.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MachinesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMachineCommand createMachineCommand)
    {
        CreatedMachineResponse response = await Mediator.Send(createMachineCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMachineCommand updateMachineCommand)
    {
        UpdatedMachineResponse response = await Mediator.Send(updateMachineCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedMachineResponse response = await Mediator.Send(new DeleteMachineCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMachineResponse response = await Mediator.Send(new GetByIdMachineQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMachineQuery getListMachineQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMachineListItemDto> response = await Mediator.Send(getListMachineQuery);
        return Ok(response);
    }
    
}
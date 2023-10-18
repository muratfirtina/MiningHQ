using Application.Features.MachineTypes.Commands.Create;
using Application.Features.MachineTypes.Commands.Delete;
using Application.Features.MachineTypes.Commands.Update;
using Application.Features.MachineTypes.Queries.GetById;
using Application.Features.MachineTypes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MachineTypesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMachineTypeCommand createMachineTypeCommand)
    {
        CreatedMachineTypeResponse response = await Mediator.Send(createMachineTypeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMachineTypeCommand updateMachineTypeCommand)
    {
        UpdatedMachineTypeResponse response = await Mediator.Send(updateMachineTypeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedMachineTypeResponse response = await Mediator.Send(new DeleteMachineTypeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMachineTypeResponse response = await Mediator.Send(new GetByIdMachineTypeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMachineTypeQuery getListMachineTypeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMachineTypeListItemDto> response = await Mediator.Send(getListMachineTypeQuery);
        return Ok(response);
    }
}
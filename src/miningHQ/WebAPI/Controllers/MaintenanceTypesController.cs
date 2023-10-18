using Application.Features.MaintenanceTypes.Commands.Create;
using Application.Features.MaintenanceTypes.Commands.Delete;
using Application.Features.MaintenanceTypes.Commands.Update;
using Application.Features.MaintenanceTypes.Queries.GetById;
using Application.Features.MaintenanceTypes.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaintenanceTypesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMaintenanceTypeCommand createMaintenanceTypeCommand)
    {
        CreatedMaintenanceTypeResponse response = await Mediator.Send(createMaintenanceTypeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMaintenanceTypeCommand updateMaintenanceTypeCommand)
    {
        UpdatedMaintenanceTypeResponse response = await Mediator.Send(updateMaintenanceTypeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedMaintenanceTypeResponse response = await Mediator.Send(new DeleteMaintenanceTypeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMaintenanceTypeResponse response = await Mediator.Send(new GetByIdMaintenanceTypeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMaintenanceTypeQuery getListMaintenanceTypeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMaintenanceTypeListItemDto> response = await Mediator.Send(getListMaintenanceTypeQuery);
        return Ok(response);
    }
}
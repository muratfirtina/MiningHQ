using Application.Features.Maintenances.Commands.Create;
using Application.Features.Maintenances.Commands.Delete;
using Application.Features.Maintenances.Commands.Update;
using Application.Features.Maintenances.Commands.UploadMaintenanceFile;
using Application.Features.Maintenances.Queries.GetById;
using Application.Features.Maintenances.Queries.GetFilesByMaintenanceId;
using Application.Features.Maintenances.Queries.GetList;
using Application.Features.Maintenances.Queries.GetListByMachineId;
using Application.Features.Maintenances.Queries.GetMaintenanceSchedule;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaintenancesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMaintenanceCommand createMaintenanceCommand)
    {
        CreatedMaintenanceResponse response = await Mediator.Send(createMaintenanceCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMaintenanceCommand updateMaintenanceCommand)
    {
        UpdatedMaintenanceResponse response = await Mediator.Send(updateMaintenanceCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedMaintenanceResponse response = await Mediator.Send(new DeleteMaintenanceCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMaintenanceResponse response = await Mediator.Send(new GetByIdMaintenanceQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMaintenanceQuery getListMaintenanceQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMaintenanceListItemDto> response = await Mediator.Send(getListMaintenanceQuery);
        return Ok(response);
    }

    [HttpGet("machine/{machineId}")]
    public async Task<IActionResult> GetListByMachineId([FromRoute] Guid machineId, [FromQuery] PageRequest pageRequest)
    {
        GetListMaintenanceByMachineIdQuery query = new() { MachineId = machineId, PageRequest = pageRequest };
        GetListResponse<GetListMaintenanceByMachineIdListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadMaintenanceFileCommand uploadMaintenanceFileCommand)
    {
        UploadMaintenanceFileDto response = await Mediator.Send(uploadMaintenanceFileCommand);
        return Ok(response);
    }

    [HttpGet("{maintenanceId}/files")]
    public async Task<IActionResult> GetFiles([FromRoute] string maintenanceId)
    {
        List<GetMaintenanceFilesDto> response = await Mediator.Send(new GetFilesByMaintenanceIdQuery { MaintenanceId = maintenanceId });
        return Ok(response);
    }

    [HttpGet("schedule")]
    public async Task<IActionResult> GetMaintenanceSchedule([FromQuery] PageRequest pageRequest)
    {
        GetMaintenanceScheduleQuery query = new() { PageRequest = pageRequest };
        GetListResponse<GetMaintenanceScheduleDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}

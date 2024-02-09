using Application.Features.EntitledLeaves.Commands.Create;
using Application.Features.EntitledLeaves.Commands.Delete;
using Application.Features.EntitledLeaves.Commands.Update;
using Application.Features.EntitledLeaves.Queries.GetByEmployeeId;
using Application.Features.EntitledLeaves.Queries.GetById;
using Application.Features.EntitledLeaves.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntitledLeavesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEntitledLeaveCommand createEntitledLeaveCommand)
    {
        CreatedEntitledLeaveResponse response = await Mediator.Send(createEntitledLeaveCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEntitledLeaveCommand updateEntitledLeaveCommand)
    {
        UpdatedEntitledLeaveResponse response = await Mediator.Send(updateEntitledLeaveCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedEntitledLeaveResponse response = await Mediator.Send(new DeleteEntitledLeaveCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdEntitledLeaveResponse response = await Mediator.Send(new GetByIdEntitledLeaveQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEntitledLeaveQuery getListEntitledLeaveQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListEntitledLeaveListItemDto> response = await Mediator.Send(getListEntitledLeaveQuery);
        return Ok(response);
    }
    
    
    [HttpGet("[action]/{employeeId}")]
    public async Task<IActionResult> GetListByEmployeeId([FromRoute] Guid employeeId,[FromQuery] PageRequest pageRequest , [FromQuery] Guid? leaveTypeId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        GetEntitledLeavesByEmployeeIdQuery getListEntitledLeaveQuery = new() { PageRequest = pageRequest, EmployeeId = employeeId, LeaveTypeId = leaveTypeId, StartDate = startDate, EndDate = endDate };
        GetListResponse<GetEmployeeEntitledLeaveDto> response = await Mediator.Send(getListEntitledLeaveQuery);
        return Ok(response);
    }
}
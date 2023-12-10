using Application.Features.LeaveTypes.Commands.Create;
using Application.Features.LeaveTypes.Commands.Delete;
using Application.Features.LeaveTypes.Commands.Update;
using Application.Features.LeaveTypes.Queries.GetList;
using Application.Features.LeaveUsages.Queries.GetById;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveTypesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLeaveTypeCommand createLeaveTypeCommand)
    {
        CreatedLeaveTypeResponse response = await Mediator.Send(createLeaveTypeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLeaveTypeCommand updateLeaveTypeCommand)
    {
        UpdatedLeaveTypeResponse response = await Mediator.Send(updateLeaveTypeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLeaveTypeResponse response = await Mediator.Send(new DeleteLeaveTypeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLeaveTypeResponse response = await Mediator.Send(new GetByIdLeaveTypeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLeaveTypeQuery getListLeaveTypeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLeaveTypesListItemDto> response = await Mediator.Send(getListLeaveTypeQuery);
        return Ok(response);
    }
}
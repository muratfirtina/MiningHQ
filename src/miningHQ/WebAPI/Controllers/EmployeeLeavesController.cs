using Application.Features.EmployeeLeaves.Commands.Create;
using Application.Features.EmployeeLeaves.Commands.Delete;
using Application.Features.EmployeeLeaves.Commands.Update;
using Application.Features.EmployeeLeaves.Queries.GetById;
using Application.Features.EmployeeLeaves.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeLeavesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEmployeeLeaveCommand createEmployeeLeaveCommand)
    {
        CreatedEmployeeLeaveResponse response = await Mediator.Send(createEmployeeLeaveCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeLeaveCommand updateEmployeeLeaveCommand)
    {
        UpdatedEmployeeLeaveResponse response = await Mediator.Send(updateEmployeeLeaveCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedEmployeeLeaveResponse response = await Mediator.Send(new DeleteEmployeeLeaveCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdEmployeeLeaveResponse response = await Mediator.Send(new GetByIdEmployeeLeaveQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEmployeeLeaveQuery getListEmployeeLeaveQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListEmployeeLeaveListItemDto> response = await Mediator.Send(getListEmployeeLeaveQuery);
        return Ok(response);
    }
}
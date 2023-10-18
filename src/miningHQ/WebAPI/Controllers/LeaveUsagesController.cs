using Application.Features.LeaveUsages.Commands.Create;
using Application.Features.LeaveUsages.Commands.Delete;
using Application.Features.LeaveUsages.Commands.Update;
using Application.Features.LeaveUsages.Queries.GetById;
using Application.Features.LeaveUsages.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveUsagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLeaveUsageCommand createLeaveUsageCommand)
    {
        CreatedLeaveUsageResponse response = await Mediator.Send(createLeaveUsageCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLeaveUsageCommand updateLeaveUsageCommand)
    {
        UpdatedLeaveUsageResponse response = await Mediator.Send(updateLeaveUsageCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLeaveUsageResponse response = await Mediator.Send(new DeleteLeaveUsageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLeaveUsageResponse response = await Mediator.Send(new GetByIdLeaveUsageQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLeaveUsageQuery getListLeaveUsageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLeaveUsageListItemDto> response = await Mediator.Send(getListLeaveUsageQuery);
        return Ok(response);
    }
}
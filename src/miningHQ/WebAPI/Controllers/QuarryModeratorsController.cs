using Application.Features.QuarryModerators.Commands.Create;
using Application.Features.QuarryModerators.Commands.Delete;
using Application.Features.QuarryModerators.Queries.GetByUserId;
using Application.Features.QuarryModerators.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Attributes;
using Domain.Constants;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuarryModeratorsController : BaseController
{
    [HttpGet]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListQuarryModeratorQuery query = new() { PageRequest = pageRequest };
        GetListResponse<GetListQuarryModeratorListItemDto> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Add([FromBody] CreateQuarryModeratorCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    [RoleAuthorize(Roles.Admin)]
    public async Task<IActionResult> Delete([FromQuery] Guid userId, [FromQuery] Guid quarryId)
    {
        var command = new DeleteQuarryModeratorCommand { UserId = userId, QuarryId = quarryId };
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    [RoleAuthorize(Roles.Admin, Roles.Moderator)]
    public async Task<IActionResult> GetUserQuarries([FromRoute] Guid userId)
    {
        var query = new GetUserQuarriesQuery { UserId = userId };
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}

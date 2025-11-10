using Application.Features.UserRoles.Commands.AssignRole;
using Application.Features.UserRoles.Commands.RemoveRole;
using Application.Features.UserRoles.Queries.GetByUserId;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserRolesController : BaseController
{
    [HttpGet("user/{UserId}")]
    public async Task<IActionResult> GetUserRoles([FromRoute] GetUserRolesQuery getUserRolesQuery)
    {
        List<GetUserRolesResponse> result = await Mediator.Send(getUserRolesQuery);
        return Ok(result);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserCommand assignRoleToUserCommand)
    {
        AssignedRoleToUserResponse result = await Mediator.Send(assignRoleToUserCommand);
        return Created(uri: "", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> RemoveRole([FromRoute] RemoveRoleFromUserCommand removeRoleFromUserCommand)
    {
        RemovedRoleFromUserResponse result = await Mediator.Send(removeRoleFromUserCommand);
        return Ok(result);
    }
}

using Application.Features.RoleOperationClaims.Commands.AssignClaim;
using Application.Features.RoleOperationClaims.Commands.RemoveClaim;
using Application.Features.RoleOperationClaims.Queries.GetByRoleId;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleOperationClaimsController : BaseController
{
    [HttpGet("role/{RoleId}")]
    public async Task<IActionResult> GetRoleClaims([FromRoute] GetRoleClaimsQuery getRoleClaimsQuery)
    {
        List<GetRoleClaimsResponse> result = await Mediator.Send(getRoleClaimsQuery);
        return Ok(result);
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignClaim([FromBody] AssignClaimToRoleCommand assignClaimToRoleCommand)
    {
        AssignedClaimToRoleResponse result = await Mediator.Send(assignClaimToRoleCommand);
        return Created(uri: "", result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> RemoveClaim([FromRoute] RemoveClaimFromRoleCommand removeClaimFromRoleCommand)
    {
        RemovedClaimFromRoleResponse result = await Mediator.Send(removeClaimFromRoleCommand);
        return Ok(result);
    }
}

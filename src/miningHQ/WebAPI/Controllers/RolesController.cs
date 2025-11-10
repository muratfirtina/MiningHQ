using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListRoleQuery getListRoleQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListRoleListItemDto> result = await Mediator.Send(getListRoleQuery);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRoleCommand createRoleCommand)
    {
        CreatedRoleResponse result = await Mediator.Send(createRoleCommand);
        return Created(uri: "", result);
    }
}

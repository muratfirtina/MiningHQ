using Application.Features.Overtimes.Commands.Create;
using Application.Features.Overtimes.Commands.Delete;
using Application.Features.Overtimes.Commands.Update;
using Application.Features.Overtimes.Queries.GetById;
using Application.Features.Overtimes.Queries.GetList;
using Application.Features.Overtimes.Queries.GetListByDynamic;
using Application.Features.Overtimes.Queries.GetOvertimeListByEmployeeId;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OvertimesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateOvertimeCommand createOvertimeCommand)
    {
        CreatedOvertimeResponse response = await Mediator.Send(createOvertimeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOvertimeCommand updateOvertimeCommand)
    {
        UpdatedOvertimeResponse response = await Mediator.Send(updateOvertimeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedOvertimeResponse response = await Mediator.Send(new DeleteOvertimeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdOvertimeResponse response = await Mediator.Send(new GetByIdOvertimeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListOvertimeQuery getListOvertimeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListOvertimeListItemDto> response = await Mediator.Send(getListOvertimeQuery);
        return Ok(response);
    }
    
    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null)
    {
        GetListByDynamicOvertimeQuery getListByDynamicOvertimeQuery = new()
        {
            DynamicQuery = dynamicQuery,
            PageRequest = pageRequest
        };
        GetListResponse<GetListByDynamicOvertimeListItemDto> response = await Mediator.Send(getListByDynamicOvertimeQuery);
        return Ok(response);
        
    }
    
    [HttpGet("GetList/ByEmployeeId")]
    public async Task<IActionResult> GetListByEmployeeId([FromQuery] PageRequest pageRequest, [FromQuery] string employeeId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        GetOvertimeListByEmployeeIdQuery query = new()
        {
            PageRequest = pageRequest,
            EmployeeId = employeeId,
            StartDate = startDate,
            EndDate = endDate
        };
        GetListResponse<GetOvertimeListByEmployeeIdListItemDto> response = await Mediator.Send(query);
        return Ok(response);
    }
}
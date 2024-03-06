using Application.Features.EmployeeLeaveUsages.Commands.Update;
using Application.Features.EmployeeLeaveUsages.Commands.Create;
using Application.Features.EmployeeLeaveUsages.Commands.Delete;
using Application.Features.EmployeeLeaveUsages.Queries.GetById;
using Application.Features.EmployeeLeaveUsages.Queries.GetList;
using Application.Features.EmployeeLeaveUsages.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeLeaveUsagesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEmployeeLeaveUsageCommand createEmployeeLeaveUsageCommand)
    {
        CreatedEmployeeLeaveUsageResponse response = await Mediator.Send(createEmployeeLeaveUsageCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeLeaveUsageCommand updateEmployeeLeaveUsageCommand)
    {
        UpdatedEmployeeLeaveUsageResponse usageResponse = await Mediator.Send(updateEmployeeLeaveUsageCommand);

        return Ok(usageResponse);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedEmployeeLeaveUsageResponse response = await Mediator.Send(new DeleteEmployeeLeaveUsageCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdEmployeeLeaveUsageResponse usageResponse = await Mediator.Send(new GetByIdEmployeeLeaveUsageQuery { Id = id });
        return Ok(usageResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEmployeeLeaveUsageQuery getListEmployeeLeaveUsageQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListEmployeeLeaveUsageListItemDto> response = await Mediator.Send(getListEmployeeLeaveUsageQuery);
        return Ok(response);
    }
    
    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null) 
    {
        
        GetListByDynamicEmployeeLeaveUsageListItemQuery getListByDynamicEmployeeLeaveUsageListItemQuery = new()
        {
            DynamicQuery = dynamicQuery,
            PageRequest = pageRequest
        };
        GetListResponse<GetListByDynamicEmployeeLeaveUsageListItemDto> response = await Mediator.Send(getListByDynamicEmployeeLeaveUsageListItemQuery);
        return Ok(response);
    }
    
   
}
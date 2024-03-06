using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetList;
using Application.Features.Employees.Queries.GetList.ShortDetail;
using Application.Features.Employees.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEmployeeCommand createEmployeeCommand)
    {
        CreatedEmployeeResponse response = await Mediator.Send(createEmployeeCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand updateEmployeeCommand)
    {
        UpdatedEmployeeResponse response = await Mediator.Send(updateEmployeeCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedEmployeeResponse response = await Mediator.Send(new DeleteEmployeeCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdEmployeeResponse response = await Mediator.Send(new GetByIdEmployeeQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEmployeeQuery getListEmployeeQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListEmployeeListItemDto> response = await Mediator.Send(getListEmployeeQuery);
        return Ok(response);
    }
    
    [HttpGet("GetList/ShortDetail")]
    public async Task<IActionResult> GetListShortDetail([FromQuery] PageRequest pageRequest)
    {
        GetListByEmplooyeeShortDetailQuery getListByEmplooyeeShortDetailQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListByEmplooyeeShortDetailItemDto> response = await Mediator.Send(getListByEmplooyeeShortDetailQuery);
        return Ok(response);
    }
    
    [HttpPost("GetList/ByDynamic")]
    public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery = null)
    {
        GetListByDynamicEmployeeQuery getListByDynamicEmployeeQuery = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery };
        GetListResponse<GetListByDynamicEmployeeListItemDto> response = await Mediator.Send(getListByDynamicEmployeeQuery);
        return Ok(response);
    }
}
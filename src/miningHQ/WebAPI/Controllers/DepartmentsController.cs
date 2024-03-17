using Application.Features.Departments.Commands.Create;
using Application.Features.Departments.Commands.Delete;
using Application.Features.Departments.Commands.Update;
using Application.Features.Departments.Queries.GetById;
using Application.Features.Departments.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDepartmentCommand createDepartmentCommand)
    {
        CreatedDepartmentResponse response = await Mediator.Send(createDepartmentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand updateDepartmentCommand)
    {
        UpdatedDepartmentResponse response = await Mediator.Send(updateDepartmentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedDepartmentResponse response = await Mediator.Send(new DeleteDepartmentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdDepartmentResponse response = await Mediator.Send(new GetByIdDepartmentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDepartmentQuery getListDepartmentQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDepartmentListItemDto> response = await Mediator.Send(getListDepartmentQuery);
        return Ok(response);
    }
}
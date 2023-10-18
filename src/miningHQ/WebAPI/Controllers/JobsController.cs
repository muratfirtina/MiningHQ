using Application.Features.Jobs.Commands.Create;
using Application.Features.Jobs.Commands.Delete;
using Application.Features.Jobs.Commands.Update;
using Application.Features.Jobs.Queries.GetById;
using Application.Features.Jobs.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateJobCommand createJobCommand)
    {
        CreatedJobResponse response = await Mediator.Send(createJobCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateJobCommand updateJobCommand)
    {
        UpdatedJobResponse response = await Mediator.Send(updateJobCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedJobResponse response = await Mediator.Send(new DeleteJobCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdJobResponse response = await Mediator.Send(new GetByIdJobQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListJobQuery getListJobQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListJobListItemDto> response = await Mediator.Send(getListJobQuery);
        return Ok(response);
    }
}
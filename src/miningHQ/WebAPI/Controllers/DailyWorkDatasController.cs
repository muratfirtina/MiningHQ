using Application.Features.DailyWorkDatas.Commands.Create;
using Application.Features.DailyWorkDatas.Commands.Delete;
using Application.Features.DailyWorkDatas.Commands.Update;
using Application.Features.DailyWorkDatas.Queries.GetById;
using Application.Features.DailyWorkDatas.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DailyWorkDatasController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDailyWorkDataCommand createDailyWorkDataCommand)
    {
        CreatedDailyWorkDataResponse response = await Mediator.Send(createDailyWorkDataCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDailyWorkDataCommand updateDailyWorkDataCommand)
    {
        UpdatedDailyWorkDataResponse response = await Mediator.Send(updateDailyWorkDataCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedDailyWorkDataResponse response = await Mediator.Send(new DeleteDailyWorkDataCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdDailyWorkDataResponse response = await Mediator.Send(new GetByIdDailyWorkDataQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDailyWorkDataQuery getListDailyWorkDataQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDailyWorkDataListItemDto> response = await Mediator.Send(getListDailyWorkDataQuery);
        return Ok(response);
    }
}
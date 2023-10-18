using Application.Features.DailyFuelConsumptionDatas.Commands.Create;
using Application.Features.DailyFuelConsumptionDatas.Commands.Delete;
using Application.Features.DailyFuelConsumptionDatas.Commands.Update;
using Application.Features.DailyFuelConsumptionDatas.Queries.GetById;
using Application.Features.DailyFuelConsumptionDatas.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DailyFuelConsumptionDatasController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDailyFuelConsumptionDataCommand createDailyFuelConsumptionDataCommand)
    {
        CreatedDailyFuelConsumptionDataResponse response = await Mediator.Send(createDailyFuelConsumptionDataCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDailyFuelConsumptionDataCommand updateDailyFuelConsumptionDataCommand)
    {
        UpdatedDailyFuelConsumptionDataResponse response = await Mediator.Send(updateDailyFuelConsumptionDataCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedDailyFuelConsumptionDataResponse response = await Mediator.Send(new DeleteDailyFuelConsumptionDataCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdDailyFuelConsumptionDataResponse response = await Mediator.Send(new GetByIdDailyFuelConsumptionDataQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListDailyFuelConsumptionDataQuery getListDailyFuelConsumptionDataQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListDailyFuelConsumptionDataListItemDto> response = await Mediator.Send(getListDailyFuelConsumptionDataQuery);
        return Ok(response);
    }
}
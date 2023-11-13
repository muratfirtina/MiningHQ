using Application.Features.Models.Commands.Create;
using Application.Features.Models.Commands.Delete;
using Application.Features.Models.Commands.Update;
using Application.Features.Models.Queries.GetById;
using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListDynamicQuery;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateModelCommand createModelCommand)
    {
        CreatedModelResponse response = await Mediator.Send(createModelCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateModelCommand updateModelCommand)
    {
        UpdatedModelResponse response = await Mediator.Send(updateModelCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedModelResponse response = await Mediator.Send(new DeleteModelCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdModelResponse response = await Mediator.Send(new GetByIdModelQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListModelListItemDto> response = await Mediator.Send(getListModelQuery);
        return Ok(response);
    }
    
    [HttpGet("ByBrand/{brandId}")]
    public async Task<IActionResult> GetListByBrandId([FromRoute] Guid brandId)
    {
        var query = new GetListByBrandIdDynamicQuery { BrandId = brandId };
        var response = await Mediator.Send(query);
        return Ok(response);
    }
}
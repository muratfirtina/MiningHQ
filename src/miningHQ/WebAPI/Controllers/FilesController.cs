using Application.Features.Files.Commands.Create;
using Application.Features.Files.Commands.Delete;
using Application.Features.Files.Commands.Update;
using Application.Features.Files.Queries.GetById;
using Application.Features.Files.Queries.GetList;
using Core.Application.Requests;
using Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FilesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFileCommand createFileCommand)
    {
        CreatedFileResponse response = await Mediator.Send(createFileCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFileCommand updateFileCommand)
    {
        UpdatedFileResponse response = await Mediator.Send(updateFileCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFileResponse response = await Mediator.Send(new DeleteFileCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFileResponse response = await Mediator.Send(new GetByIdFileQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFileQuery getListFileQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFileListItemDto> response = await Mediator.Send(getListFileQuery);
        return Ok(response);
    }
}
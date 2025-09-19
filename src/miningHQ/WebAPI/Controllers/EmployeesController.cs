using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Commands.UpdateShowcase;
using Application.Features.Employees.Commands.UploadEmployeeFile;
using Application.Features.Employees.Commands.UploadEmployeePhoto;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetEmployeePhoto;
using Application.Features.Employees.Queries.GetEmployeePhotoBase64;
using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Application.Features.Employees.Queries.GetList;
using Application.Features.Employees.Queries.GetList.ShortDetail;
using Application.Features.Employees.Queries.GetListByDynamic;
using Application.Services.Repositories;
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
    
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromForm] UploadEmployeeFileCommand uploadEmployeeFileCommand)
    {
        UploadEmployeeFileDto response = await Mediator.Send(uploadEmployeeFileCommand);
        return Ok(response);
    }
    
    // EmployeeId'ye ait resimleri getir
    [HttpGet("[action]/{employeeId}")]
    public async Task<IActionResult> GetImagesByEmployeeId([FromRoute] string employeeId)
    {
        var response = await Mediator.Send(new GetFilesByEmployeeIdQuery { EmployeeId =employeeId});
        return Ok(response);
    }
    
    
    [HttpPost("[action]")]
       public async Task<IActionResult> UploadEmployeePhoto([FromForm] UploadEmployeePhotoCommand uploadEmployeePhotoCommand)
       {
           UploadEmployeePhotoResponse response = await Mediator.Send(uploadEmployeePhotoCommand);
           return Ok(response);
       }

    
    [HttpGet("[action]/{employeeId}")]
    public async Task<IActionResult> GetEmployeePhoto([FromRoute] string employeeId)
    {
        // ⭐ CQRS pattern'ine uygun olarak Mediator kullanıyoruz
        GetEmployeePhotoResponse response = await Mediator.Send(new GetEmployeePhotoQuery { EmployeeId = employeeId });
        return Ok(response);
    }
    
    
    [HttpGet("[action]")]
    public async Task<IActionResult> ChangeShowcase([FromQuery] UpdateShowcaseCommand updateShowcaseCommand)
    {
        UpdateShowcaseResponse response = await Mediator.Send(updateShowcaseCommand);
        return Ok(response);
    }
    
    // Presentation/Controllers/EmployeesController.cs'e bu endpoint'i ekleyin

    [HttpGet("get-employee-photo-base64/{employeeId}")]
    public async Task<IActionResult> GetEmployeePhotoBase64([FromRoute] string employeeId)
    {
        try
        {
            var query = new GetEmployeePhotoBase64Query { EmployeeId = employeeId };
            var response = await Mediator.Send(query);

            if (response == null || !response.Success)
            {
                return NotFound(new { 
                    message = response?.Message ?? "Employee photo not found",
                    success = false 
                });
            }

            return Ok(new { 
                base64 = response.Base64,
                mimeType = response.MimeType,
                fileSize = response.FileSize,
                success = response.Success,
                message = response.Message,
                // Opsiyonel ek bilgiler
                id = response.Id,
                employeeId = response.EmployeeId,
                name = response.Name,
                url = response.Url
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { 
                message = $"Error: {ex.Message}",
                success = false 
            });
        }
    }
     
}
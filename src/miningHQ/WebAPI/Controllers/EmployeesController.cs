using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetList;
using Application.Features.Employees.Queries.GetList.ShortDetail;
using Application.Features.Employees.Queries.GetListByDynamic;
using Application.Services.ImageService;
using Application.Storage.Cloudinary;
using Application.Storage.Local;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILocalStorage _localStorage;
    private readonly ICloudinaryStorage _cloudinaryStorage;

    public EmployeesController(IWebHostEnvironment webHostEnvironment, ILocalStorage localStorage, ICloudinaryStorage cloudinaryStorage)
    {
        _webHostEnvironment = webHostEnvironment;
        _localStorage = localStorage;
        _cloudinaryStorage = cloudinaryStorage;
    }

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
    
    /*[HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromForm] IFormFileCollection files)
    {
        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            string imageUrl = await _imageService.UploadAsync(file);
            datas.Add((file.FileName, imageUrl));
        }
        return Ok(datas);
    }*/
    
    /*[HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromForm] string path, [FromForm] IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", path);
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            // await anahtar kelimesi ile asenkron işlemi bekleyin
            var filePath = Path.Combine(uploadPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // Dosya kopyalama işlemini asenkron olarak bekleyin
            }
            datas.Add((file.FileName, Path.Combine("images", path, file.FileName))); // Dosya yolu bilgisini güncelleyin
        }
        return Ok(datas);
    }*/
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromForm] string path,[FromForm] IFormFileCollection files)
    {
        List<(string fileName, string path)> datas = new();
        foreach (IFormFile file in files)
        {
            var imageUploadResult = await _cloudinaryStorage.UploadAsync(path, files);
            datas.Add((file.FileName, imageUploadResult.FirstOrDefault().path));
            
        }
        return Ok(datas);
    }



    
     
}
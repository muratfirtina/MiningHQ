using Application.Features.Employees.Commands.Create;
using Application.Features.Employees.Commands.Delete;
using Application.Features.Employees.Commands.Update;
using Application.Features.Employees.Commands.UpdateShowcase;
using Application.Features.Employees.Commands.UploadEmployeeFile;
using Application.Features.Employees.Queries.GetById;
using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Application.Features.Employees.Queries.GetList;
using Application.Features.Employees.Queries.GetList.ShortDetail;
using Application.Features.Employees.Queries.GetListByDynamic;
using Application.Services.ImageService;
using Application.Services.Repositories;
using Application.Storage;
using Application.Storage.Azure;
using Application.Storage.Cloudinary;
using Application.Storage.Google;
using Application.Storage.Local;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Domain.Entities;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : BaseController
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly ILocalStorage _localStorage;
    private readonly ICloudinaryStorage _cloudinaryStorage;
    private readonly IFileRepository _fileRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IStorageService _storageService;
    private readonly IAzureStorage _azureStorage;
    private readonly IGoogleStorage _googleStorage;
    

    public EmployeesController(IWebHostEnvironment webHostEnvironment, ILocalStorage localStorage, ICloudinaryStorage cloudinaryStorage, IFileRepository fileRepository, IEmployeeRepository employeeRepository, IStorageService storageService, IAzureStorage azureStorage, IGoogleStorage googleStorage)
    {
        _webHostEnvironment = webHostEnvironment;
        _localStorage = localStorage;
        _cloudinaryStorage = cloudinaryStorage;
        _fileRepository = fileRepository;
        _employeeRepository = employeeRepository;
        _storageService = storageService;
        _azureStorage = azureStorage;
        _googleStorage = googleStorage;
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
    
    
    [HttpPost("[action]")]
    public async Task<IActionResult> Upload([FromForm] UploadEmployeeFileCommand uploadEmployeeFileCommand)
    {
        UploadEmployeeFileDto response = await Mediator.Send(uploadEmployeeFileCommand);
        return Ok(response);
    }
    
    /*[HttpPost("[action]")]
    public async Task<IActionResult> UploadEmployeePhoto([FromForm] string category, [FromForm] string path, [FromForm] IFormFileCollection files, [FromForm] string employeeId)
    {
        if(files == null || files.Count == 0)
            return BadRequest("Dosya bulunamadı.");
        
        // Yükleme işleminden önce, çalışanın mevcut fotoğraflarını silin
        var file = await _employeeRepository.GetEmployeePhoto(employeeId);
        if (file != null)
        {
            
            await _cloudinaryStorage.DeleteAsync(file.Path);
            await _azureStorage.DeleteAsync(file.Path);
            await _localStorage.DeleteAsync(_webHostEnvironment.WebRootPath + "/images/" + category +"/" + file.Name);   
            await _fileRepository.DeleteAsync(file);
        }
        

        // Cloudinary'ye dosya yükleme işlemini burada gerçekleştirin
        
        var employee = await _employeeRepository.GetAsync(x => x.Id == Guid.Parse(employeeId));
        if (employee == null)
            return BadRequest("Çalışan bulunamadı.");
        
        //gelen category ve dosya adını düzenle.
        
        var uploadResults = await _googleStorage.UploadAsync(category,path, files);
        
        await _localStorage.UploadAsync(category,path, files);

        

        foreach (var uploadResult in uploadResults)
        {
            // Yüklenen her bir dosya için veritabanında bir kayıt oluşturun
            await _fileRepository.AddAsync(new EmployeePhoto()
            {
                Name = uploadResult.fileName,
                Path = uploadResult.path,
                Category = uploadResult.containerName,
                Storage = StorageType.Google.ToString(),
                Employee = employee
            });
        }

        return Ok();
    }*/

    
    [HttpGet("[action]/{employeeId}")]
    public async Task<IActionResult> GetEmployeePhoto([FromRoute] string employeeId)
    {
        var photo = await _employeeRepository.GetEmployeePhoto(employeeId);
        return Ok(photo);
    }
    
    
    // EmployeeId'ye ait resimleri getir
    [HttpGet("[action]/{employeeId}")]
    public async Task<IActionResult> GetImagesByEmployeeId([FromRoute] string employeeId)
    {
        var response = await Mediator.Send(new GetFilesByEmployeeIdQuery { EmployeeId = employeeId });
        return Ok(response);
    }
    
    
    [HttpGet("[action]")]
    public async Task<IActionResult> ChangeShowcase([FromQuery] UpdateShowcaseCommand updateShowcaseCommand)
    {
        UpdateShowcaseResponse response = await Mediator.Send(updateShowcaseCommand);
        return Ok(response);
    }
    
     
}
using Application.Services.Repositories;
using Application.Storage;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuarryFilesController : BaseController
{
    private readonly IStorageService _storageService;
    private readonly IQuarryFileRepository _quarryFileRepository;
    private readonly IQuarryRepository _quarryRepository;

    public QuarryFilesController(
        IStorageService storageService, 
        IQuarryFileRepository quarryFileRepository,
        IQuarryRepository quarryRepository)
    {
        _storageService = storageService;
        _quarryFileRepository = quarryFileRepository;
        _quarryRepository = quarryRepository;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] IFormFileCollection files, [FromForm] string quarryId)
    {
        if (files == null || files.Count == 0)
            return BadRequest("No files uploaded");

        if (string.IsNullOrEmpty(quarryId))
            return BadRequest("Quarry ID is required");

        // Validate quarry exists
        var quarry = await _quarryRepository.GetAsync(q => q.Id == Guid.Parse(quarryId));
        if (quarry == null)
            return NotFound("Quarry not found");

        var fileList = files.ToList();
        var category = "quarry-files";
        var path = quarryId;

        // Upload files using storage service
        var uploadedFiles = await _storageService.UploadAsync(category, path, fileList);

        // Save to database with quarry relationship
        var quarryFileEntities = new List<QuarryFile>();
        foreach (var uploadedFile in uploadedFiles)
        {
            var quarryFile = new QuarryFile
            {
                Id = Guid.NewGuid(),
                Name = uploadedFile.fileName,
                Category = category, // ✅ Category eklendi
                Path = uploadedFile.path,
                Storage = uploadedFile.storageType,
                Showcase = false,
                CreatedDate = DateTime.UtcNow,
                Quarries = new List<Quarry> { quarry }
            };

            await _quarryFileRepository.AddAsync(quarryFile);
            quarryFileEntities.Add(quarryFile);
        }

        var result = quarryFileEntities.Select(f => new
        {
            Id = f.Id,
            FileName = f.Name,
            Path = f.Path,
            Storage = f.Storage,
            Category = f.Category
        }).ToList();

        return Ok(new { Files = result, Message = "Files uploaded successfully" });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string path)
    {
        if (string.IsNullOrEmpty(path))
            return BadRequest("Path is required");

        // Find file in database
        var file = await _quarryFileRepository.GetAsync(f => f.Path == path);
        
        if (file != null)
        {
            // Delete from storage
            await _storageService.DeleteAsync(path);
            
            // Delete from database
            await _quarryFileRepository.DeleteAsync(file);
            
            return Ok(new { Message = "File deleted successfully" });
        }
        
        return NotFound("File not found");
    }

    [HttpGet("exists")]
    public IActionResult CheckFileExists([FromQuery] string path, [FromQuery] string fileName)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(fileName))
            return BadRequest("Path and fileName are required");

        bool exists = _storageService.HasFile(path, fileName);
        
        return Ok(new { Exists = exists });
    }

    [HttpGet("by-quarry/{quarryId}")]
    public async Task<IActionResult> GetByQuarry([FromRoute] string quarryId)
    {
        if (string.IsNullOrEmpty(quarryId))
            return BadRequest("Quarry ID is required");

        var files = await _quarryFileRepository.GetListAsync(
            predicate: f => f.Quarries.Any(q => q.Id == Guid.Parse(quarryId))
        );

        var result = files.Items.Select(f => new
        {
            Id = f.Id,
            FileName = f.Name,
            Path = f.Path,
            Storage = f.Storage,
            Category = f.Category,
            CreatedDate = f.CreatedDate
        }).ToList();

        return Ok(result);
    }
}

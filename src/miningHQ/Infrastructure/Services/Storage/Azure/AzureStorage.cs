using Application.Services.Files;
using Application.Services.Repositories;
using Application.Storage;
using Application.Storage.Azure;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using File = Domain.Entities.File;

namespace Infrastructure.Services.Storage.Azure;

public class AzureStorage : IAzureStorage
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IFileNameService _fileNameService;
    private readonly StorageSettings _storageSettings;

    public Task<Base64FileResult?> GetFileAsBase64Async(string category, string path, string fileName) => throw new NotImplementedException();

    public string StorageName => "AzureStorage";

    public AzureStorage(IEmployeeRepository employeeRepository, IOptions<StorageSettings> storageSettings, IFileNameService fileNameService)
    {
        _employeeRepository = employeeRepository;
        _fileNameService = fileNameService;
        _storageSettings = storageSettings.Value;
    }

    public async Task<List<(string fileName, string path, string category, string storageType)>> UploadAsync(string category, string path, List<IFormFile> files)
    {
        // TODO: Implement Azure Blob Storage upload
        throw new NotImplementedException("Azure Storage upload not implemented yet");
    }

    public async Task DeleteAsync(string path)
    {
        // TODO: Implement Azure Blob Storage delete
        throw new NotImplementedException("Azure Storage delete not implemented yet");
    }

    public async Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new()
    {
        // TODO: Implement Azure Blob Storage file listing
        throw new NotImplementedException("Azure Storage get files not implemented yet");
    }

    public bool HasFile(string path, string fileName)
    {
        // TODO: Implement Azure Blob Storage file existence check
        return false;
    }

    public async Task FileMustBeInImageFormat(IFormFile formFile)
    {
        List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp", ".heic" };
        string extension = Path.GetExtension(formFile.FileName).ToLower();
        
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported image format");
        
        await Task.CompletedTask;
    }
    
    public async Task FileMustBeInFileFormat(IFormFile formFile)
    {
        List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".heic" };
        string extension = Path.GetExtension(formFile.FileName).ToLower();
        
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported file format");
        
        await Task.CompletedTask;
    }
}
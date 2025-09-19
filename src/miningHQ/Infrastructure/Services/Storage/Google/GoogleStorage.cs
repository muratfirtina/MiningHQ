using Application.Enums;
using Application.Services.Files;
using Application.Services.Repositories;
using Application.Storage;
using Application.Storage.Google;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using File = Domain.Entities.File;

namespace Infrastructure.Services.Storage.Google;

public class GoogleStorage : IGoogleStorage
{
    private readonly StorageClient _storageClient;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IFileNameService _fileNameService;
    private readonly StorageSettings _storageSettings;
    private readonly string _bucketName = "mininghq";

    public Task<Base64FileResult?> GetFileAsBase64Async(string category, string path, string fileName) => throw new NotImplementedException();

    public string StorageName => "GoogleStorage";

    public GoogleStorage(IConfiguration configuration, IEmployeeRepository employeeRepository, IOptions<StorageSettings> storageSettings, IFileNameService fileNameService)
    {
        _employeeRepository = employeeRepository;
        _fileNameService = fileNameService;
        _storageSettings = storageSettings.Value;

        var credentialsPath = configuration["Storage:Google:CredentialsFilePath"];
        if (string.IsNullOrEmpty(credentialsPath))
        {
            throw new BusinessException("Google Cloud Storage service account key file path is not configured.");
        }

        var credential = GoogleCredential.FromFile(credentialsPath);
        _storageClient = StorageClient.Create(credential);
    }

    public async Task<List<(string fileName, string path, string category, string storageType)>> UploadAsync(string category, string path, List<IFormFile> files)
    {
        List<(string fileName, string path, string category, string storageType)> results = new();
        
        foreach (var file in files)
        {
            await FileMustBeInFileFormat(file);
            
            string newPath = await _fileNameService.PathRenameAsync(path);
            string fileNewName = await _fileNameService.FileRenameAsync(newPath, file.FileName, HasFile);
            
            var objectName = $"{category}/{newPath}/{fileNewName}";
            
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            
            await _storageClient.UploadObjectAsync(_bucketName, objectName, file.ContentType, memoryStream);
            
            results.Add((fileNewName, newPath, category, StorageType.Google.ToString()));
        }
        
        return results;
    }

    public async Task DeleteAsync(string path)
    {
        try
        {
            await _storageClient.DeleteObjectAsync(_bucketName, path);
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Failed to delete file from Google Storage: {ex.Message}");
        }
    }

    public async Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new()
    {
        var baseUrl = _storageSettings.GoogleStorageUrl;
        
        if (string.IsNullOrEmpty(employeeId))
        {
            return null;
        }
        
        var employeeFiles = await _employeeRepository.GetFilesByEmployeeId(employeeId);
        if (employeeFiles == null || !employeeFiles.Any())
            return null; 

        List<T> files = new List<T>();

        foreach (var employeeFileDto in employeeFiles) 
        {
            var prefix = $"{employeeFileDto.Category}/{employeeFileDto.Path}/";
            var objects = _storageClient.ListObjects(_bucketName, prefix);

            var matchingObject = objects.FirstOrDefault(obj => obj.Name.EndsWith(employeeFileDto.FileName)); 

            if (matchingObject != null)
            {
                var file = new T
                {
                    Id = employeeFileDto.Id,
                    Name = employeeFileDto.FileName,
                    Path = employeeFileDto.Path,
                    Category = employeeFileDto.Category,
                    Storage = employeeFileDto.Storage, 
                    Url = $"{baseUrl}/{matchingObject.Name}"
                };
                files.Add(file);
            }
        }

        return files;
    }

    public bool HasFile(string path, string fileName)
    {
        try
        {
            var obj = _storageClient.GetObject(_bucketName, $"{path}/{fileName}");
            return obj != null;
        }
        catch (Exception)
        {
            return false;
        }
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
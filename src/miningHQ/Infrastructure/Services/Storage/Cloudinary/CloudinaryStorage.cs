using Application.Enums;
using Application.Services.Files;
using Application.Services.Repositories;
using Application.Storage;
using Application.Storage.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions; // ⭐ SearchParams için gerekli import
using Core.CrossCuttingConcerns.Exceptions.Types;
using Infrastructure.Adapters.ImageService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using File = Domain.Entities.File;

namespace Infrastructure.Services.Storage.Cloudinary;

public class CloudinaryStorage : ICloudinaryStorage
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IFileNameService _fileNameService;
    private readonly StorageSettings _storageSettings;

    public Task<Base64FileResult?> GetFileAsBase64Async(string category, string path, string fileName) => throw new NotImplementedException();

    public string StorageName => "CloudinaryStorage";
    
    public CloudinaryStorage(IConfiguration configuration, IEmployeeRepository employeeRepository, IOptions<StorageSettings> storageSettings, IFileNameService fileNameService)
    {
        _employeeRepository = employeeRepository;
        _fileNameService = fileNameService;
        _storageSettings = storageSettings.Value;
        
        var cloudinaryAccountSettings = configuration.GetSection("CloudinaryAccount").Get<CloudinarySettings>();

        var account = new Account(
            cloudinaryAccountSettings?.CloudName,
            cloudinaryAccountSettings?.ApiKeyName,
            cloudinaryAccountSettings?.ApiSecretName
        );

        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<List<(string fileName, string path, string category, string storageType)>> UploadAsync(string category, string path, List<IFormFile> files)
    {
        List<(string fileName, string path, string category, string storageType)> results = new();
        
        foreach (var file in files)
        {
            await FileMustBeInFileFormat(file);
            
            string newPath = await _fileNameService.PathRenameAsync(path);
            string fileNewName = await _fileNameService.FileRenameAsync(newPath, file.FileName, HasFile);
            
            ImageUploadParams imageUploadParams = new()
            {
                File = new FileDescription(fileNewName, stream: file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = false,
                Folder = $"{category}/{newPath}"
            };

            ImageUploadResult imageUploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            
            if (imageUploadResult.Error != null)
            {
                throw new BusinessException($"Cloudinary upload failed: {imageUploadResult.Error.Message}");
            }
            
            results.Add((fileNewName, newPath, category, StorageType.Cloudinary.ToString()));
        }
        
        return results;
    }

    public async Task DeleteAsync(string path)
    {
        try
        {
            var publicId = GetPublicId(path);
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);
            
            if (result.Error != null)
            {
                throw new BusinessException($"Cloudinary delete failed: {result.Error.Message}");
            }
        }
        catch (Exception ex)
        {
            throw new BusinessException($"Failed to delete file from Cloudinary: {ex.Message}");
        }
    }

    public async Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new()
    {
        if (string.IsNullOrEmpty(employeeId))
        {
            return null;
        }
        
        var employeeFiles = await _employeeRepository.GetFilesByEmployeeId(employeeId);
        if (employeeFiles == null || !employeeFiles.Any())
            return null;

        List<T> files = new List<T>();

        foreach (var employeeFileDto in employeeFiles.Where(f => f.Storage == StorageType.Cloudinary.ToString()))
        {
            var file = new T
            {
                Id = employeeFileDto.Id,
                Name = employeeFileDto.FileName,
                Path = employeeFileDto.Path,
                Category = employeeFileDto.Category,
                Storage = employeeFileDto.Storage,
                Url = $"https://res.cloudinary.com/{_cloudinary.Api.Account.Cloud}/image/upload/{employeeFileDto.Category}/{employeeFileDto.Path}/{employeeFileDto.FileName}"
            };
            files.Add(file);
        }

        return files;
    }

    public bool HasFile(string path, string fileName) 
        => System.IO.File.Exists(Path.Combine(path, fileName));

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

    private string GetPublicId(string imageUrl)
    {
        var uploadSegment = "/image/upload/";
        var startIndex = imageUrl.IndexOf(uploadSegment) + uploadSegment.Length;
        if(startIndex > uploadSegment.Length - 1)
        {
            var pathWithVersion = imageUrl.Substring(startIndex);
            var pathStartIndex = pathWithVersion.IndexOf('/', 1);
            if(pathStartIndex > -1)
            {
                var publicId = pathWithVersion.Substring(pathStartIndex + 1);
                var endIndex = publicId.LastIndexOf('.');
                if(endIndex > -1)
                {
                    publicId = publicId.Substring(0, endIndex);
                }
                return publicId;
            }
        }
        return string.Empty;
    }
}
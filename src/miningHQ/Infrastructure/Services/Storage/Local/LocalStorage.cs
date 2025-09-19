// Infrastructure/Services/Storage/Local/LocalStorage.cs - Mevcut class'a bu method'u ekleyin

using Application.Enums;
using Application.Services.Files;
using Application.Services.Repositories;
using Application.Storage;
using Application.Storage.Local;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using File = Domain.Entities.File;

namespace Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    // Mevcut constructor ve field'lar aynı kalacak...
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IFileNameService _fileNameService;
    private readonly string _baseFolderPath = Path.Combine("wwwroot");
    private readonly StorageSettings _storageSettings;
    
    public string StorageName => "LocalStorage";

    public LocalStorage(IEmployeeRepository employeeRepository, IOptions<StorageSettings> storageSettings, IFileNameService fileNameService)
    {
        _employeeRepository = employeeRepository;
        _fileNameService = fileNameService;
        _storageSettings = storageSettings.Value;
        
        if (!Directory.Exists(_baseFolderPath))
        {
            Directory.CreateDirectory(_baseFolderPath);
        }
    }

    // ⭐ YENİ METHOD: Base64 dönüştürme
    public async Task<Base64FileResult?> GetFileAsBase64Async(string category, string path, string fileName)
    {
        try
        {
            var physicalPath = Path.Combine(_baseFolderPath, category, path, fileName);

            if (!System.IO.File.Exists(physicalPath))
            {
                return null;
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(physicalPath);
            var base64String = Convert.ToBase64String(bytes);

            // MIME type'ı belirle
            var extension = Path.GetExtension(physicalPath).ToLower();
            var mimeType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".bmp" => "image/bmp",
                ".svg" => "image/svg+xml",
                ".heic" => "image/heic",
                _ => "image/jpeg"
            };

            return new Base64FileResult
            {
                Base64 = base64String,
                MimeType = mimeType,
                FileSize = bytes.Length
            };
        }
        catch (Exception ex)
        {
            // Log the error if you have logging
            return null;
        }
    }

    // Mevcut method'lar aynı kalacak (UploadAsync, DeleteAsync, vs.)
    public async Task<List<(string fileName, string path, string category, string storageType)>> UploadAsync(string category, string path, List<IFormFile> files)
    {
        // Mevcut implementasyon...
        List<(string fileName, string path, string category, string storageType)> results = new();
        
        foreach (var file in files)
        {
            await FileMustBeInFileFormat(file);
            
            string newPath = await _fileNameService.PathRenameAsync(path);
            string fileNewName = await _fileNameService.FileRenameAsync(newPath, file.FileName, HasFile);
            
            var employeeFolderPath = Path.Combine(_baseFolderPath, category, newPath);
            if (!Directory.Exists(employeeFolderPath))
            {
                Directory.CreateDirectory(employeeFolderPath);
            }
            
            var fullFilePath = Path.Combine(employeeFolderPath, fileNewName);
            await SaveFileAsync(fullFilePath, file);
            
            results.Add((fileNewName, newPath, category, StorageType.Local.ToString()));
        }
        
        return results;
    }

    public async Task DeleteAsync(string path)
    {
        var filePath = Path.Combine(_baseFolderPath, path);
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
        }
    }

    public async Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new()
    {
        var baseUrl = _storageSettings.LocalStorageUrl;
        
        if (string.IsNullOrEmpty(employeeId))
        {
            return null;
        }
        
        var files = await _employeeRepository.GetFilesByEmployeeId(employeeId);
        return files.Select(file => new T
        {
            Name = file.FileName,
            Path = file.Path,
            Category = file.Category,
            Storage = file.Storage,
            Id = file.Id,
            Url = $"{baseUrl}/{file.Category}/{file.Path}/{file.FileName}"
        }).ToList();
    }

    public bool HasFile(string path, string fileName) 
        => System.IO.File.Exists(Path.Combine(_baseFolderPath, path, fileName));

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

    private async Task SaveFileAsync(string filePath, IFormFile file)
    {
        try
        {
            await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: true);
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
        }
        catch (Exception ex)
        {
            throw new BusinessException($"File save failed: {ex.Message}");
        }
    }
}
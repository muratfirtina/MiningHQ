using Microsoft.AspNetCore.Http;
using File = Domain.Entities.File;

namespace Application.Storage;

public interface IStorageService
{
    /// <summary>
    /// Upload multiple files to storage
    /// </summary>
    Task<List<(string fileName, string path, string category, string storageType)>> UploadAsync(string category, string path, List<IFormFile> files);
    
    /// <summary>
    /// Delete a file from storage
    /// </summary>
    Task DeleteAsync(string path);
    
    /// <summary>
    /// Get all files for a specific employee
    /// </summary>
    Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new();
    
    /// <summary>
    /// Check if a file exists in storage
    /// </summary>
    bool HasFile(string path, string fileName);
    
    /// <summary>
    /// Validate file format for images only
    /// </summary>
    Task FileMustBeInImageFormat(IFormFile formFile);
    
    /// <summary>
    /// Validate file format for all supported file types
    /// </summary>
    Task FileMustBeInFileFormat(IFormFile formFile);

    /// <summary>
    /// ⭐ Get file as Base64 string for PDF generation
    /// </summary>
    Task<Base64FileResult?> GetFileAsBase64Async(string category, string path, string fileName);
    
    /// <summary>
    /// Storage provider name
    /// </summary>
    string StorageName { get; }
}

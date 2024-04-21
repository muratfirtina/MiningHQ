using Microsoft.AspNetCore.Http;
using File = Domain.Entities.File;

namespace Application.Storage;

public interface IStorage
{
    Task<List<(string fileName, string path, string category,string storageType)>> UploadAsync(string category, string path,
        List<IFormFile> files);
    Task DeleteAsync(string path);
    Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new();
    bool HasFile(string path, string fileName);
}
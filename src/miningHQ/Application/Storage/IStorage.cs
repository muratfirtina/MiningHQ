using Microsoft.AspNetCore.Http;

namespace Application.Storage;

public interface IStorage
{
    Task<List<(string fileName, string path, string category,string storageType)>> UploadAsync(string category, string path,
        List<IFormFile> files);
    Task DeleteAsync(string path);
    List<string> GetFiles(string path);
    bool HasFile(string path, string fileName);
}
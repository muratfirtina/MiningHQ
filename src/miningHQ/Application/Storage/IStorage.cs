using Microsoft.AspNetCore.Http;

namespace Application.Storage;

public interface IStorage
{
    Task<List<(string fileName, string path, string containerName)>> UploadAsync(string category,string path, IFormFileCollection files);
    Task DeleteAsync(string path);
    List<string> GetFiles(string path);
    bool HasFile(string path, string fileName);
}
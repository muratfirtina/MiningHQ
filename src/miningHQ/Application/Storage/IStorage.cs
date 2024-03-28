using Microsoft.AspNetCore.Http;

namespace Application.Storage;

public interface IStorage
{
    Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files);
    Task DeleteAsync(string path, string fileName);
    List<string> GetFiles(string path);
    bool HasFile(string path, string fileName);
}
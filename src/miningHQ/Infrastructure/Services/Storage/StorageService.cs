using Application.Storage;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage;

public class StorageService : IStorageService
{
    readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public Task<List<(string fileName, string path)>> UploadAsync(string category, string pathOrContainerName, IFormFileCollection files)
        => _storage.UploadAsync(category,pathOrContainerName, files);

    public async Task DeleteAsync(string path) 
        => await _storage.DeleteAsync(path);

    public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName);

    public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

    public string StorageName { get => _storage.GetType().Name ; }
}
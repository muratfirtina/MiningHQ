using File = Domain.Entities.File;

namespace Application.Storage;

public interface IBlobService
{
    Task<List<(string fileName, string path, string containerName)>> UploadFileToStorage(string category, string path, string fileName, MemoryStream fileStream);
    
    Task DeleteAsync(string path);
    Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new();
    bool HasFile(string path, string fileName);
}
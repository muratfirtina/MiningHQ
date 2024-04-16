namespace Application.Storage;

public interface IBlobService
{
    Task<List<(string fileName, string path, string containerName)>> UploadFileToStorage(string category, string path, string fileName, MemoryStream fileStream);
    
    Task DeleteAsync(string path);
    List<string> GetFiles(string path);
    bool HasFile(string path, string fileName);
}
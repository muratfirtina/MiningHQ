using Application.Storage.Google;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Storage.Google;

public class GoogleStorage : FileService, IGoogleStorage
{
    private readonly StorageClient _storageClient;

    public GoogleStorage(IConfiguration configuration)
    {
        
        var credentialsPath = configuration["Storage:Google:CredentialsFilePath"];
        if (string.IsNullOrEmpty(credentialsPath))
        {
            throw new BusinessException("Google Cloud Storage service account key file path is not configured.");
        }
        

        var credential = GoogleCredential.FromFile(credentialsPath);
        _storageClient = StorageClient.Create(credential);
    }

    public async Task<List<(string fileName, string path, string containerName)>> UploadAsync(string category, string path, IFormFileCollection files)
    {
        List<(string fileName, string path, string containerName)> datas = new();
        
        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(path, file.FileName, HasFile);
            await FileMustBeInFileFormat(file);
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                await _storageClient.UploadObjectAsync("mininghq", $"{category}/{path}/{fileNewName}", null, memoryStream);
                //datas.Add((file.FileName, System.IO.Path.Combine(path, fileNewName), category));
            }
        }
        return datas;

    }

    public async Task DeleteAsync(string path)
    {
        var storage = StorageClient.Create();
        await storage.DeleteObjectAsync("mininghq", path);
    }

    public List<string> GetFiles(string path)
    {
        var storage = StorageClient.Create();
        var steam = storage.ListObjects("mininghq", path);
        return steam.Select(x => x.Name).ToList();
        
    }



    public bool HasFile(string path, string fileName)
    {
        try
        {
            // Google Cloud Storage'da belirtilen bucket ve dosya adıyla bir obje olup olmadığını kontrol edin
            var obj = _storageClient.GetObject("mininghq", $"{path}/{fileName}");
            return obj != null; // Eğer obje null değilse, dosya var demektir
        }
        
        catch (Exception ex)
        {
            // Diğer hatalar için bir loglama yapısı ekleyebilirsiniz
            // Bu örnek için basitçe false dönüyoruz, ancak gerçek bir uygulamada hata yönetimi daha detaylı olmalıdır
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
    
    public async Task FileMustBeInFileFormat(IFormFile formFile)
    {
        List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".heic" };

        string extension = Path.GetExtension(formFile.FileName).ToLower();
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported format");
        await Task.CompletedTask;
    }

}
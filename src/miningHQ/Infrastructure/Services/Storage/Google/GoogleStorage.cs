using Application.Services.Repositories;
using Application.Storage.Google;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using File = Domain.Entities.File;

namespace Infrastructure.Services.Storage.Google;

public class GoogleStorage : IGoogleStorage
{
    private readonly StorageClient _storageClient;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly StorageSettings _storageSettings;
    private readonly string _bucketName = "mininghq";

    public GoogleStorage(IConfiguration configuration, IEmployeeRepository employeeRepository, IOptions<StorageSettings> storageSettings)
    {
        _employeeRepository = employeeRepository;
        _storageSettings = storageSettings.Value;

        var credentialsPath = configuration["Storage:Google:CredentialsFilePath"];
        if (string.IsNullOrEmpty(credentialsPath))
        {
            throw new BusinessException("Google Cloud Storage service account key file path is not configured.");
        }
        

        var credential = GoogleCredential.FromFile(credentialsPath);
        _storageClient = StorageClient.Create(credential);
    }


    public async Task<List<(string fileName, string path, string containerName)>> UploadFileToStorage(string category,
        string path, string fileName, MemoryStream fileStream)
    {
        List<(string fileName, string path, string containerName)> datas = new();
        await _storageClient.UploadObjectAsync("mininghq", $"{category}/{path}/{fileName}", null, fileStream);
        
        return null;
        
        
    }

    public async Task DeleteAsync(string path)
    {
        var storage = StorageClient.Create();
        await storage.DeleteObjectAsync("mininghq", path);
    }

    public async Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new()
    {
        var baseUrl = _storageSettings.GoogleStorageUrl; // Ayarlardan URL alınır
        //employeeId ye göre category ve path bul.
        var employeeFiles = await _employeeRepository.GetFilesByEmployeeId(employeeId);
        if (employeeFiles == null || !employeeFiles.Any())
            return null; 

        List<T> files = new List<T>();

        foreach (var employeeFileDto in employeeFiles) 
        {
            var prefix = $"{employeeFileDto.Category}/{employeeFileDto.Path}/";
            var objects = _storageClient.ListObjects(_bucketName, prefix);

            // Assuming your _storageClient handles finding the relevant file based on the metadata
            var matchingObject = objects.FirstOrDefault(obj => obj.Name.EndsWith(employeeFileDto.FileName)); 

            if (matchingObject != null)
            {
                var file = new T
                {
                    Id = employeeFileDto.Id,
                    Name = employeeFileDto.FileName,
                    Path = employeeFileDto.Path,
                    Category = employeeFileDto.Category,
                    Storage = employeeFileDto.Storage, 
                    Url = $"{baseUrl}/{matchingObject.Name}"
                };
                files.Add(file);
            }
        }

        return files;
    }
        
    

    /*
    public Task<List<T>> GetFiles<T>(string category, string path) where T : File, new() => throw new NotImplementedException();
    */

    /*public async Task<List<string?>> GetFiles(string path, string category)
    {
        var storage = StorageClient.Create();
        var steam = storage.ListObjects("mininghq", path);
        List<string> files = new();
        foreach (var obj in steam)
        {
            files.Add(obj.Name);
        }
        return files;
        
    }*/



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
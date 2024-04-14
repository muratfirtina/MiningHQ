using Application.Storage;
using Application.Storage.Azure;
using Application.Storage.Cloudinary;
using Application.Storage.Google;
using Application.Storage.Local;
using Infrastructure.Services.Storage.Azure;
using Infrastructure.Services.Storage.Cloudinary;
using Infrastructure.Services.Storage.Google;
using Infrastructure.Services.Storage.Local;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage;

public class StorageService : FileService, IStorageService
{
    private readonly ILocalStorage _localStorage;
    private readonly ICloudinaryStorage _cloudinaryStorage;
    private readonly IAzureStorage _azureStorage;
    private readonly IGoogleStorage _googleStorage;
    readonly IStorage _storage;

    public StorageService(IStorage storage, ILocalStorage localStorage, ICloudinaryStorage cloudinaryStorage, IAzureStorage azureStorage, IGoogleStorage googleStorage)
    {
        _storage = storage;
        _localStorage = localStorage;
        _cloudinaryStorage = cloudinaryStorage;
        _azureStorage = azureStorage;
        _googleStorage = googleStorage;
    }

    public async Task<List<(string fileName, string path, string containerName)>> UploadAsync(string category, string path,
        IFormFileCollection files)
    {
        string newPath = await PathRenameAsync(path);
        
        // Tüm storage servislerin upload işlemlerini burada topluyoruz. Bir dosyayı aynı anda birden fazla storage servisine yükleyebiliriz.
        List<(string fileName, string path, string containerName)> datas = new();
        datas.AddRange(await _localStorage.UploadAsync(category, newPath, files));
        //datas.AddRange(await _cloudinaryStorage.UploadAsync(category, path, files));
        datas.AddRange(await _azureStorage.UploadAsync(category, newPath, files));
        
        datas.AddRange(await _googleStorage.UploadAsync(category, newPath, files));
        return datas;

        
    }

    public async Task DeleteAsync(string path) 
        => await _storage.DeleteAsync(path);

    public List<string> GetFiles(string path)
        => _storage.GetFiles(path);

    public bool HasFile(string path, string fileName)
        => _storage.HasFile(path, fileName);

    public string StorageName { get => _storage.GetType().Name ; }
}
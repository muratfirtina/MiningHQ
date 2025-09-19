using Application.Enums;
using Application.Storage;
using Application.Storage.Cloudinary;
using Application.Storage.Google;
using Application.Storage.Local;
using Application.Storage.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Storage;

public class StorageFactory : IStorageFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public StorageFactory(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
    }

    public IStorageService CreateStorageService(StorageType storageType)
    {
        return storageType switch
        {
            StorageType.Local => _serviceProvider.GetRequiredService<ILocalStorage>(),
            StorageType.Cloudinary => _serviceProvider.GetRequiredService<ICloudinaryStorage>(),
            StorageType.Google => _serviceProvider.GetRequiredService<IGoogleStorage>(),
            StorageType.Azure => _serviceProvider.GetRequiredService<IAzureStorage>(),
            _ => _serviceProvider.GetRequiredService<ILocalStorage>()
        };
    }

    public IStorageService GetDefaultStorageService()
    {
        // Configuration'dan default storage type'ı al
        var defaultStorageType = _configuration.GetValue<string>("StorageUrls:StorageProvider");
        
        if (Enum.TryParse<StorageType>(defaultStorageType, out var storageType))
        {
            return CreateStorageService(storageType);
        }
        
        // Fallback to Local if configuration is missing
        return CreateStorageService(StorageType.Local);
    }

    public IStorageService GetStorageServiceByName(string storageTypeName)
    {
        if (Enum.TryParse<StorageType>(storageTypeName, true, out var storageType))
        {
            return CreateStorageService(storageType);
        }
        
        throw new ArgumentException($"Unknown storage type: {storageTypeName}");
    }
}
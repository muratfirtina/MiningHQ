using Amazon.S3;
using Application.Services;
using Application.Services.Files;
using Application.Services.ImageService;
using Application.Storage;
using Application.Storage.AWS;
using Application.Storage.Azure;
using Application.Storage.Cloudinary;
using Application.Storage.Google;
using Application.Storage.Local;
using Infrastructure.Adapters.ImageService;
using Infrastructure.Enums;
using Infrastructure.Services;
using Infrastructure.Services.Storage;
using Infrastructure.Services.Storage.AWS;
using Infrastructure.Services.Storage.Azure;
using Infrastructure.Services.Storage.Cloudinary;
using Infrastructure.Services.Storage.Google;
using Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ILocalStorage, LocalStorage>();
        services.AddScoped<ICloudinaryStorage, CloudinaryStorage>();
        services.AddScoped<IAzureStorage, AzureStorage>();
        services.AddScoped<IGoogleStorage, GoogleStorage>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IFileNameService, FileNameService>();
        
        return services;
    }
    
    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : class,IBlobService
    {
        serviceCollection.AddScoped<IBlobService, T>();
        serviceCollection.AddScoped<IStorage,StorageService>();
    }
    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IBlobService, LocalStorage>();
                break;
            
            case StorageType.Cloudinary:
                serviceCollection.AddScoped<IBlobService, CloudinaryStorage>();
                break;
            case StorageType.Google:
                serviceCollection.AddScoped<IBlobService, GoogleStorage>();
                break;
            /*case StorageType.Aws:
                serviceCollection.AddScoped<IBlobService, AwsStorage>();
                break;*/
            default:
                serviceCollection.AddScoped<IBlobService, LocalStorage>();
                break;
        }
    }
}

using Application.Enums;
using Application.Services.Files;
using Application.Storage;
using Application.Storage.Azure;
using Application.Storage.Cloudinary;
using Application.Storage.Google;
using Application.Storage.Local;
using Infrastructure.Services;
using Infrastructure.Services.Storage;
using Infrastructure.Services.Storage.Azure;
using Infrastructure.Services.Storage.Cloudinary;
using Infrastructure.Services.Storage.Google;
using Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // ✅ FACTORY PATTERN İLE STORAGE REGISTRATION
        services.AddScoped<ILocalStorage, LocalStorage>();
        services.AddScoped<ICloudinaryStorage, CloudinaryStorage>();
        services.AddScoped<IGoogleStorage, GoogleStorage>();
        services.AddScoped<IAzureStorage, AzureStorage>();
    
        // Register Storage Factory
        services.AddScoped<IStorageFactory, StorageFactory>();
    
        // ⭐ BACKWARD COMPATIBILITY - ESKİ KODLAR İÇİN
        services.AddScoped<IStorageService, LocalStorage>();
    
        // Register other services
        services.AddScoped<IFileNameService, FileNameService>();
    
        // Configuration
        services.Configure<StorageSettings>(configuration.GetSection("StorageUrls"));
        return services;
    }
    
}
using Application.Services;
using Application.Services.ImageService;
using Application.Storage;
using Infrastructure.Adapters.ImageService;
using Infrastructure.Enums;
using Infrastructure.Services;
using Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ImageServiceBase, CloudinaryImageServiceAdapter>();
        services.AddScoped<Application.Storage.Local.ILocalStorage, LocalStorage>();
        services.AddScoped<ILocalStorage, LocalImageService>();
        return services;
    }
    
    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : FileService, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
    {
        switch (storageType)
        {
            case StorageType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            /*case StorageType.Azure:
                serviceCollection.AddScoped<IStorage, AzureStorage>();
                break;
            case StorageType.AWS:
                serviceCollection.AddScoped<IStorage, AWSStorage>();
                break;*/
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}

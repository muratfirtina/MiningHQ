using Application.Enums;

namespace Application.Storage;

public interface IStorageFactory
{
    IStorageService CreateStorageService(StorageType storageType);
    IStorageService GetDefaultStorageService();
    IStorageService GetStorageServiceByName(string storageTypeName);
}
using Application.Storage.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Storage.Azure;

public class AzureStorage : FileService, IAzureStorage

{
    readonly BlobServiceClient _blobServiceClient;
    BlobContainerClient _blobContainerClient;

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string category, string path,
        IFormFileCollection files)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        _blobContainerClient.CreateIfNotExists();
        _blobContainerClient.SetAccessPolicy(PublicAccessType.BlobContainer);
        List<(string fileName, string path)> datas = new();

        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(path, file.FileName, HasFile);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
            blobClient.Upload(file.OpenReadStream());
            datas.Add((fileNewName, System.IO.Path.Combine(path, fileNewName)));

        }
        
        return datas;

    }

    public bool HasFile(string path, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        return blobClient.Exists();
    }
    
    public async Task DeleteAsync(string path)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        BlobClient blobClient = _blobContainerClient.GetBlobClient(path);
        await blobClient.DeleteAsync();
    }
    
    public List<string> GetFiles(string path)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }
}
using Application.Storage.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Storage.Azure;

public class AzureStorage : IAzureStorage

{
    readonly BlobServiceClient _blobServiceClient;
    BlobContainerClient _blobContainerClient;

    public AzureStorage(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["Storage:Azure"]);
    }

    /*public async Task<List<(string fileName, string path, string containerName)>> UploadAsync(string category,
        string path,
        List<IFormFile> files)
    {
        
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        await _blobContainerClient.CreateIfNotExistsAsync();
        _blobContainerClient.SetAccessPolicy(PublicAccessType.BlobContainer);
        

        foreach (IFormFile file in files)
        {
            BlobClient blobClient = _blobContainerClient.GetBlobClient(file.FileName);
            
            blobClient.Upload(file.OpenReadStream());
            //datas.Add((file.FileName, System.IO.Path.Combine(newPath, fileNewName)));

        }
        return null;

    }*/
    public async Task<List<(string fileName, string path, string containerName)>> UploadFileToStorage(string category,
        string path, string fileName, MemoryStream fileStream)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(path);
        _blobContainerClient.CreateIfNotExists();
        _blobContainerClient.SetAccessPolicy(PublicAccessType.BlobContainer);
        
        BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream);
        return null;
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
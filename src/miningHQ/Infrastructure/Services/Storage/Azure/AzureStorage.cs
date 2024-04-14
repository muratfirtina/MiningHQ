using Application.Storage.Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.CrossCuttingConcerns.Exceptions.Types;
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

    public async Task<List<(string fileName, string path, string containerName)>> UploadAsync(string category, string path,
        IFormFileCollection files)
    {
        string newPath = await PathRenameAsync(path);
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(newPath);
        _blobContainerClient.CreateIfNotExists();
        _blobContainerClient.SetAccessPolicy(PublicAccessType.BlobContainer);
        List<(string fileName, string path)> datas = new();

        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(newPath, file.FileName, HasFile);
            await FileMustBeInFileFormat(file);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
            
            blobClient.Upload(file.OpenReadStream());
            //datas.Add((file.FileName, System.IO.Path.Combine(newPath, fileNewName)));

        }
        
        return datas.Select(d => (d.fileName, d.path, category)).ToList();

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
    
    public async Task FileMustBeInFileFormat(IFormFile formFile)
    {
        List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".heic" };

        string extension = Path.GetExtension(formFile.FileName).ToLower();
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported format");
        await Task.CompletedTask;
    }
}
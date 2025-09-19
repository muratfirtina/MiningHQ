using Amazon.S3;
using Application.Storage;
using Application.Storage.AWS;
using Microsoft.AspNetCore.Http;
using File = Domain.Entities.File;

namespace Infrastructure.Services.Storage.AWS;

public class AwsStorage:IStorageService
{
    private readonly IAmazonS3 _s3Client;

    public AwsStorage(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    /*public async Task<List<(string fileName, string path, string containerName)>> UploadAsync(string category,
        string path,
        List<IFormFile> files)
    {
        List<(string fileName, string path, string containerName)> datas = new();
        
        foreach (IFormFile file in files)
        {
            
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                //await _s3Client.UploadObjectFromStreamAsync("mininghq", $"{category}/{path}/{fileNewName}", memoryStream, null);
                //datas.Add((file.FileName, System.IO.Path.Combine(path, fileNewName), category));
            }
        }
        return datas;
    }*/

    public async Task<List<(string fileName, string path, string containerName)>> UploadFileToStorage(string category,
        string path, string fileName, MemoryStream fileStream)
    {
        List<(string fileName, string path, string containerName)> datas = new();
        await _s3Client.UploadObjectFromStreamAsync("mininghq", $"{category}/{path}/{fileName}", fileStream, null);
        
        return null;
    }

    public Task<List<(string fileName, string path, string category, string storageType)>> UploadAsync(string category, string path, List<IFormFile> files) => throw new NotImplementedException();

    public async Task DeleteAsync(string path)
    {
           await _s3Client.DeleteObjectAsync("mininghq", path);
    }

    public Task<List<T>?> GetFiles<T>(string employeeId) where T : File, new() => throw new NotImplementedException();

    public Task<List<T>> GetFiles<T>(string category, string path) where T : File, new() => throw new NotImplementedException();

    public async Task<List<string?>> GetFiles(string path, string category)
    {
        var response = await _s3Client.ListObjectsAsync("mininghq", $"{path}");
        var files = response.S3Objects.Select(x => x.Key).ToList();
        return files;
    }

    public bool HasFile(string path, string fileName)
    {
        var response = _s3Client.GetObjectMetadataAsync("mininghq", path);
        return response != null;
    }

    public Task FileMustBeInImageFormat(IFormFile formFile) => throw new NotImplementedException();

    public Task FileMustBeInFileFormat(IFormFile formFile) => throw new NotImplementedException();
    public Task<Base64FileResult?> GetFileAsBase64Async(string category, string path, string fileName) => throw new NotImplementedException();

    public string StorageName { get; }
}
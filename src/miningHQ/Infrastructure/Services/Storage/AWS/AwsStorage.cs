using Amazon.S3;
using Application.Storage.AWS;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage.AWS;

public class AwsStorage:IAwsStorage
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

    public async Task DeleteAsync(string path)
    {
           await _s3Client.DeleteObjectAsync("mininghq", path);
    }

    public List<string> GetFiles(string path)
    {
        _s3Client.ListObjectsAsync("mininghq", path);
        return new List<string>();
    }

    public bool HasFile(string path, string fileName)
    {
        var response = _s3Client.GetObjectMetadataAsync("mininghq", path);
        return response != null;
    }
}
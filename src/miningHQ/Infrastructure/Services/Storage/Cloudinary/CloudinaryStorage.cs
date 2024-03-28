using Application.Storage.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Infrastructure.Adapters.ImageService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.Storage.Cloudinary;

public class CloudinaryStorage : FileService, ICloudinaryStorage
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;
    
    public CloudinaryStorage(IConfiguration configuration)
    {
        var cloudinaryAccountSettings = configuration.GetSection("CloudinaryAccount").Get<CloudinarySettings>();

        var account = new Account(
            cloudinaryAccountSettings?.CloudName,
            cloudinaryAccountSettings?.ApiKeyName,
            cloudinaryAccountSettings?.ApiSecretName
        );

        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        var datas = new List<(string fileName, string pathOrContainerName)>();
        foreach (IFormFile file in files)
        {
            ImageUploadParams imageUploadParams = new()
            {
                File = new FileDescription(file.FileName, stream: file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = false
            };
            string fileNewName = await FileRenameAsync(path, file.FileName, HasFile);
            
            
            imageUploadParams.Folder = path;
            imageUploadParams.File.FileName = fileNewName;

            ImageUploadResult imageUploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            datas.Add((file.FileName, imageUploadResult.Url.ToString()));
            
        }
        return datas;
        
    }

    public Task DeleteAsync(string path, string fileName)
    {
        DeletionParams deletionParams = new(path + "/" + fileName);
        return _cloudinary.DestroyAsync(deletionParams);
        
    }

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        return directoryInfo.GetFiles().Select(f =>f.Name).ToList();
    }

    public bool HasFile(string path, string fileName) 
        => File.Exists(Path.Combine(path, fileName));
    
    private string GetPublicId(string imageUrl)
    {
        int startIndex = imageUrl.LastIndexOf('/') + 1;
        int endIndex = imageUrl.LastIndexOf('.');
        int length = endIndex - startIndex;
        return imageUrl.Substring(startIndex, length);
    }
}
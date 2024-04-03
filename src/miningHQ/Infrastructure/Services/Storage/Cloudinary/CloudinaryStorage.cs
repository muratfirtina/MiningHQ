using Application.Storage.Cloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.CrossCuttingConcerns.Exceptions.Types;
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

    public async Task<List<(string fileName, string path)>> UploadAsync(string category,string path, IFormFileCollection files)
    {
        var datas = new List<(string fileName, string path)>();
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
            
            await FileMustBeInFileFormat(file);
            
            imageUploadParams.Folder = category + "/" + path;
            imageUploadParams.File.FileName = fileNewName;

            ImageUploadResult imageUploadResult = await _cloudinary.UploadAsync(imageUploadParams);
            datas.Add((file.FileName, imageUploadResult.Url.ToString()));
            
        }
        return datas;
        
    }
    
    

    public async Task DeleteAsync(string path)
    {
        var publicId = GetPublicId(path);
        var deletionParams = new DeletionParams(publicId);
        await _cloudinary.DestroyAsync(deletionParams);
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
        // Cloudinary URL'sinden '/image/upload/' dizgisinden sonraki kısmı çıkarmak için
        var uploadSegment = "/image/upload/";
        var startIndex = imageUrl.IndexOf(uploadSegment) + uploadSegment.Length;
        if(startIndex > uploadSegment.Length - 1)
        {
            // '/image/upload/' segmentinden sonraki kısmı alır
            var pathWithVersion = imageUrl.Substring(startIndex);
        
            // 'version' kısmını geçmek için ikinci '/' karakterinden sonrasını alır
            var pathStartIndex = pathWithVersion.IndexOf('/', 1);
            if(pathStartIndex > -1)
            {
                var publicId = pathWithVersion.Substring(pathStartIndex + 1);

                // Eğer varsa dosya uzantısını kaldırır
                var endIndex = publicId.LastIndexOf('.');
                if(endIndex > -1)
                {
                    publicId = publicId.Substring(0, endIndex);
                }

                return publicId;
            }
        }

        // Eğer URL beklenen formatta değilse, boş bir string dön
        return string.Empty;
    }
    
    public async Task FileMustBeInFileFormat(IFormFile formFile)
    {
        List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp", ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".heic"};

        string extension = Path.GetExtension(formFile.FileName).ToLower();
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported format");
        await Task.CompletedTask;
    }


    
}
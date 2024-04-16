using Application.Storage.Local;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage.Local;

public class LocalStorage : ILocalStorage
{
    private readonly string _baseFolderPath = Path.Combine("wwwroot");
    
    public LocalStorage()
    {
        if (!Directory.Exists(_baseFolderPath))
        {
            Directory.CreateDirectory(_baseFolderPath);
        }
    }
    /*public async Task<List<(string fileName, string path, string containerName)>> UploadAsync(string category,
        string path, List<IFormFile> files)
    {
        
        var employeeFolderPath = Path.Combine(_baseFolderPath, category, path);
        if (!Directory.Exists(employeeFolderPath))
        {
            Directory.CreateDirectory(employeeFolderPath);
        }
        
        List<(string fileName, string path, string containerName)> datas = new ();
        foreach (IFormFile file in files)
        {
            
            await CopyFileAsync(Path.Combine(employeeFolderPath, file.FileName), file);
            datas.Add((file.FileName, Path.Combine(path, file.FileName), category));
        }
        return datas;
        
    }*/


    public async Task<List<(string fileName, string path, string containerName)>> UploadFileToStorage(string category,
        string path, string fileName, MemoryStream fileStream)
    {
        var employeeFolderPath = Path.Combine(_baseFolderPath, category, path);
        
        if (!Directory.Exists(employeeFolderPath))
        {
            Directory.CreateDirectory(employeeFolderPath);
        }
        
        List<(string fileName, string path, string containerName)> datas = new();
        
        //dosyayı locale kaydet
        var filePath = Path.Combine(employeeFolderPath, fileName);
        await using FileStream fileStream1 = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync:false);
        await fileStream.CopyToAsync(fileStream1);
        await fileStream1.FlushAsync();
        
        datas.Add((fileName, path, category));

        return null;
    }

    public async Task DeleteAsync(string path)
    {
        //var localPath = ExtractLocalPath(path); // Dosya yolu çıkar
        var filePath = Path.Combine(_baseFolderPath, path); // Dosya yolu ve adını birleştir
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        
    }

    public List<string> GetFiles(string path)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(path);
        return directoryInfo.GetFiles().Select(f =>f.Name).ToList();
    }

    public bool HasFile(string path, string fileName) 
        => File.Exists(Path.Combine(path, fileName));
    
    async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync:false);
            
            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;

        }
        catch (Exception e)
        {
            //todo: loglama yapılacak!
            throw e;
        }
        

    }
    
    public string ExtractLocalPath(string fullPath)
    {
        // Örneğin, Cloudinary URL'si veya karmaşık bir dosya yolu verildiğinde,
        // yerel dosya sistemindeki klasör yapısına ve dosya adına karşılık gelen kısmı çıkarmak için
        // bir mantık geliştirmeniz gerekebilir.
    
        // Örnek fullPath:
        // "/Users/muratfirtina/Projects/GitHub/MiningHQFull/MiningHQ/src/miningHQ/WebAPI/wwwroot/images/http:/res.cloudinary.com/mininghq/image/upload/v1712050438/employee-images/LÜTFİ BURSALI/a64d8339-c5bf-469c-9d0e-681abed1e15b-20240402093356.jpg/_a64d8339-c5bf-469c-9d0e-681abed1e15b.jpeg"

        // fullPath içindeki 'wwwroot' kelimesinden sonra gelen kısmı almak için
        var startIndex = fullPath.IndexOf("wwwroot") + "wwwroot".Length;
        if(startIndex > -1)
        {
            var relativePath = fullPath.Substring(startIndex);

            // Eğer URL'den dosya adını çıkarmak istiyorsanız, ek işlemler yapılabilir
            // Örneğin, son '/' karakterinden sonrasını almak gibi
            var lastIndex = relativePath.LastIndexOf('/');
            if(lastIndex > -1)
            {
                relativePath = relativePath.Substring(0, lastIndex); // Son dosya adını kaldır
            }

            // Temizlenmiş yerel yol
            return relativePath;
        }

        // Eğer fullPath beklenen formatta değilse, orijinal fullPath'i döndür veya uygun bir hata yönetimi yap
        return fullPath;
    }
    
    public async Task FileMustBeInImageFormat(IFormFile formFile)
    {
        List<string> extensions = new() { ".jpg", ".png", ".jpeg", ".webp", ".heic" };

        string extension = Path.GetExtension(formFile.FileName).ToLower();
        if (!extensions.Contains(extension))
            throw new BusinessException("Unsupported format");
        await Task.CompletedTask;
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
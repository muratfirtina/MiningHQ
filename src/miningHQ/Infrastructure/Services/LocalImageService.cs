using Application.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class LocalImageService : ILocalStorage
{
    private readonly string _baseFolderPath = Path.Combine("wwwroot", "images");

    public LocalImageService()
    {
        if (!Directory.Exists(_baseFolderPath))
        {
            Directory.CreateDirectory(_baseFolderPath);
        }
    }

    public async Task<string> UploadAsync(IFormFile formFile, string employeeFullName)
    {
        //KlasörAdı Çalışanın adı olsun.
        var employeeFolderPath = Path.Combine(_baseFolderPath, employeeFullName);
        if (!Directory.Exists(employeeFolderPath))
        {
            Directory.CreateDirectory(employeeFolderPath);
        }

        string filePath = Path.Combine(employeeFolderPath, Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName));
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
        }
        return filePath;
    }

    public  async Task DeleteAsync(string imageUrl)
    {
        if (File.Exists(imageUrl))
        {
            File.Delete(imageUrl);
        }
        await Task.CompletedTask;
    }

    
}
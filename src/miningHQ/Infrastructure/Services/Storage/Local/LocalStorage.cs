using Application.Storage.Local;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services.Storage.Local;

public class LocalStorage : FileService, ILocalStorage
{
    private readonly string _baseFolderPath = Path.Combine("wwwroot", "images");
    
    public LocalStorage()
    {
        if (!Directory.Exists(_baseFolderPath))
        {
            Directory.CreateDirectory(_baseFolderPath);
        }
    }
    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        var employeeFolderPath = Path.Combine(_baseFolderPath, path);
        if (!Directory.Exists(employeeFolderPath))
        {
            Directory.CreateDirectory(employeeFolderPath);
        }
        
        List<(string fileName, string path)> datas = new ();
        foreach (IFormFile file in files)
        {
            string fileNewName = await FileRenameAsync(path, file.FileName, HasFile);
            
            await CopyFileAsync(Path.Combine(employeeFolderPath, fileNewName), file);
            datas.Add((file.FileName, Path.Combine(path, fileNewName)));
        }
        return datas;
        
    }

    public async Task DeleteAsync(string path, string fileName)
    {
        File.Delete(Path.Combine(path, fileName));
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
}
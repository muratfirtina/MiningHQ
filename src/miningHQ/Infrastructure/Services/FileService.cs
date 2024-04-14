using Core.CrossCuttingConcerns.Exceptions.Types;
using Infrastructure.Operations;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Infrastructure.Services;

public class FileService

{
    protected delegate bool HasFile(string pathOrContainerName, string fileName);
    protected async Task<string> FileRenameAsync(string pathOrContainerName,string fileName, HasFile hasFileMethod)
    {
        string extension = Path.GetExtension(fileName);
        string oldName = Path.GetFileNameWithoutExtension(fileName);
        string regulatedFileName = NameOperation.CharacterRegulatory(oldName);
        regulatedFileName = regulatedFileName.ToLower().Trim('-', ' '); //harfleri küçültür ve baştaki ve sondaki - ve boşlukları siler
        //oldName = oldName.Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u").Replace(" ", "-");
        //char[] invalidChars = { '$', ':', ';', '@', '+', '-', '_', '=', '(', ')', '{', '}', '[', ']' ,'∑','€','®','₺','¥','π','¨','~','æ','ß','∂','ƒ','^','∆','´','¬','Ω','√','∫','µ','≥','÷','|'}; //geçersiz karakterleri belirler.
        //oldName = oldName.TrimStart(invalidChars).TrimEnd(invalidChars); //baştaki ve sondaki geçersiz karakterleri siler
        //Regex regex = new Regex("[*'\",+._&#^@|/<>~]");
        //string regulatedFileName = NameOperation.CharacterRegulatory(oldName);
        //string newFileName = regex.Replace(regulatedFileName, string.Empty);//geçersiz karakterleri siler ve yeni dosya ismi oluşturur.
        DateTime datetimenow = DateTime.UtcNow;
        string datetimeutcnow = datetimenow.ToString("yyyyMMddHHmmss");//dosya isminin sonuna eklenen tarih bilgisi
        string fullName = $"{regulatedFileName}-{extension}";//dosya ismi ve uzantısı birleştirilir ve yeni dosya ismi oluşturulur.

        if (hasFileMethod(pathOrContainerName, fullName))
        {
            int i = 1;
            while (hasFileMethod(pathOrContainerName, fullName))
            {
                fullName = $"{regulatedFileName}-{extension}";
                i++;
            }
        }

        return fullName;
    }

    protected async Task<string> PathRenameAsync(string pathOrContainerName)
    {
        string regulatedPath = NameOperation.CharacterRegulatory(pathOrContainerName);
        regulatedPath = regulatedPath.ToLower().Trim('-', ' '); //harfleri küçültür ve baştaki ve sondaki - ve boşlukları siler
        //pathOrContainerName = pathOrContainerName.Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i").Replace("ö", "o").Replace("ş", "s").Replace("ü", "u").Replace(" ", "-");
        //char[] invalidChars = { '$', ':', ';', '@', '+', '-', '_', '=', '(', ')', '{', '}', '[', ']', '∑', '€', '®', '₺', '¥', 'π', '¨', '~', 'æ', 'ß', '∂', 'ƒ', '^', '∆', '´', '¬', 'Ω', '√', '∫', 'µ', '≥', '÷', '|' }; //geçersiz karakterleri belirler.
        //pathOrContainerName = pathOrContainerName.TrimStart(invalidChars).TrimEnd(invalidChars); //baştaki ve sondaki geçersiz karakterleri siler
        //Regex regex = new Regex("[*'\",+._&#^@|/<>~]");
        //string newPath = regex.Replace(regulatedPath, string.Empty);//geçersiz karakterleri siler ve yeni dosya ismi oluşturur.
        return regulatedPath;
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
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public interface ILocalStorage
{
    Task<string> UploadAsync(IFormFile formFile, string employeeId);
    Task DeleteAsync(string imageUrl);

    
   
    
}
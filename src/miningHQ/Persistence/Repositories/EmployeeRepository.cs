using Application.Features.Employees.Dtos;
using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmployeeRepository : EfRepositoryBase<Employee, Guid, MiningHQDbContext>, IEmployeeRepository
{
    private readonly IConfiguration _configuration;
    public EmployeeRepository(MiningHQDbContext context, IConfiguration configuration) : base(context)
    {
        _configuration = configuration;
    }
    
    public async Task<List<EmployeeWithTimekeepingsDto>> GetEmployeesWithTimekeepings(int year, int month, int pageIndex, int pageSize)
    {
        // EF Core sorgusu kullanarak tüm personelleri ve ilgili Timekeeping kayıtlarını getirin
        var query = Context.Employees
            .Select(e => new EmployeeWithTimekeepingsDto
            {
                EmployeeId = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                HireDate = e.HireDate,
                Timekeepings = e.Timekeepings
                    .Where(tk => tk.Date.Year == year && tk.Date.Month == month)
                    .Select(tk => new TimekeepingDto
                    {
                        Date = tk.Date,
                        Status = tk.Status
                    }).ToList()
            }).ToListAsync();

        return await query;
    }

    public async Task<List<GetEmployeeFilesDto>> GetFilesByEmployeeId(string employeeId)
    {
        var storageProvider = _configuration["StorageProvider"];
        var azureStorageUrl = _configuration["AzureStorageUrl"];
        var googleStorageUrl = _configuration["GoogleStorageUrl"];
        //var awsStorageUrl = _configuration["AwsStorageUrl"];
        var localStorageUrl = _configuration["LocalStorageUrl"];;
        
        var query = Context.Employees
            .Where(e => e.Id == Guid.Parse(employeeId))
            .SelectMany(e => e.EmployeeFiles)
            .OrderByDescending(e => e.CreatedDate)
            .Select(e => new GetEmployeeFilesDto
            {
                FileName = e.Name,
                Path = storageProvider == "AzureStorage" ? $"{azureStorageUrl}{e.Path}" :
                    storageProvider == "GoogleStorage" ? $"{googleStorageUrl}{e.Category}/{e.Path}" :
                    //storageProvider == "AwsStorage" ? $"{awsStorageUrl}{e.Category}/{e.Path}" :
                    storageProvider == "LocalStorage" ? $"{localStorageUrl}{e.Category}/{e.Path}" : e.Path,
                Showcase = e.Showcase,
                Storage = storageProvider,
                Category = e.Category,
                Id = e.Id.ToString()
            }).ToListAsync();

        return await query;
    }
    
    //showcase değerini değiştir.
    public async Task ChangeShowcase(string employeeId, string fileId ,bool showcase)
    {
        var query = Context.Employees
            .Include(e => e.EmployeeFiles)
            .SelectMany(e => e.EmployeeFiles, (e, ef) => new
            {
                e, ef
            });

        var data = await query.FirstOrDefaultAsync(p => p.e.Id == Guid.Parse(employeeId) && p.ef.Showcase);
        
        if(data !=null) data.ef.Showcase = false;

        var image = await query.FirstOrDefaultAsync(p => p.ef.Id == Guid.Parse(fileId));

        if (image != null) image.ef.Showcase = true;

        await Context.SaveChangesAsync();
    }

    public async Task<EmployeePhoto?> GetEmployeePhoto(string employeeId)
    {
        
        var query = Context.Employees
            .Where(e => e.Id == Guid.Parse(employeeId))
            .Select(e => e.EmployeePhoto)
            .FirstOrDefaultAsync();

        return await query;
        
    }
    
    
}
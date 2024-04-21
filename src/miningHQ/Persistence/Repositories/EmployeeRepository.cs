using Application.Features.Employees.Dtos;
using Application.Features.Employees.Queries.GetById;
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
    public EmployeeRepository(MiningHQDbContext context) : base(context)
    {
        
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
        
        var query = Context.Employees
            .Where(e => e.Id == Guid.Parse(employeeId))
            .SelectMany(e => e.EmployeeFiles)
            .OrderByDescending(e => e.CreatedDate)
            .Select(ef => new GetEmployeeFilesDto
            {
                FileName = ef.Name,
                Path = ef.Path,
                Showcase = ef.Showcase,
                Storage = ef.Storage,
                Category = ef.Category,
                Id = ef.Id,
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
    
    public async Task<GetByIdEmployeeResponse> GetEmployeeWithFiles(string employeeId)
    {
        var query = Context.Employees
            .Where(e => e.Id == Guid.Parse(employeeId))
            .Select(e => new GetByIdEmployeeResponse
            {
                
                FirstName = e.FirstName,
                LastName = e.LastName,
                EmployeeFiles = e.EmployeeFiles
                    .Select(ef => new EmployeeFile
                    {
                        Id = ef.Id,
                        Name = ef.Name,
                        Path = ef.Path,
                        Category = ef.Category,
                        Storage = ef.Storage,
                        Showcase = ef.Showcase
                    }).ToList()
            }).FirstOrDefaultAsync();

        return await query;
    }
    
    
}
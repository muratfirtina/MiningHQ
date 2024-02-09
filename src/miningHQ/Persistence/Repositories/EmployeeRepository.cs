using Application.Features.Employees.Dtos;
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
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
}
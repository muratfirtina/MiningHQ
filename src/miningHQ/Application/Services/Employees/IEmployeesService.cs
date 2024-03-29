using Application.Features.Employees.Dtos;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Employees;

public interface IEmployeesService
{
    Task<Employee?> GetAsync(
        Expression<Func<Employee, bool>> predicate,
        Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Employee>?> GetListAsync(
        Expression<Func<Employee, bool>>? predicate = null,
        Func<IQueryable<Employee>, IOrderedQueryable<Employee>>? orderBy = null,
        Func<IQueryable<Employee>, IIncludableQueryable<Employee, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Employee> AddAsync(Employee employee);
    Task<Employee> UpdateAsync(Employee employee);
    Task<Employee> DeleteAsync(Employee employee, bool permanent = false);
    
    Task<List<EmployeeWithTimekeepingsDto>> GetEmployeesWithTimekeepings(int year, int month, int pageIndex, int pageSize);
}

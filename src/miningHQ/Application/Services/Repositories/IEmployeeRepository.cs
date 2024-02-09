using Application.Features.Employees.Dtos;
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IEmployeeRepository : IAsyncRepository<Employee, Guid>, IRepository<Employee, Guid>
{
    Task<List<EmployeeWithTimekeepingsDto>> GetEmployeesWithTimekeepings(int year, int month, int pageIndex, int pageSize);
}
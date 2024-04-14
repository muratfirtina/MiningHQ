using Application.Features.Employees.Dtos;
using Application.Features.Employees.Queries.GetFilesByEmployeeId;
using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IEmployeeRepository : IAsyncRepository<Employee, Guid>, IRepository<Employee, Guid>
{
    Task<List<EmployeeWithTimekeepingsDto>> GetEmployeesWithTimekeepings(int year, int month, int pageIndex, int pageSize);
    Task<List<GetEmployeeFilesDto>> GetFilesByEmployeeId(string employeeId);
    Task ChangeShowcase(string employeeId, string fileId,bool showcase);
    Task<EmployeePhoto?> GetEmployeePhoto(string employeeId);
}
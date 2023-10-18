using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmployeeRepository : EfRepositoryBase<Employee, Guid, MiningHQDbContext>, IEmployeeRepository
{
    public EmployeeRepository(MiningHQDbContext context) : base(context)
    {
    }
}
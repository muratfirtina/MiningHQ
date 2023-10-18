using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmployeeLeaveRepository : EfRepositoryBase<EmployeeLeave, Guid, MiningHQDbContext>, IEmployeeLeaveRepository
{
    public EmployeeLeaveRepository(MiningHQDbContext context) : base(context)
    {
    }
}
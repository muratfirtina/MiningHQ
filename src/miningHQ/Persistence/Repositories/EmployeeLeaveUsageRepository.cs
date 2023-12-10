using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class EmployeeLeaveUsageRepository : EfRepositoryBase<EmployeeLeaveUsage, Guid, MiningHQDbContext>, IEmployeeLeaveUsageRepository
{
    public EmployeeLeaveUsageRepository(MiningHQDbContext context) : base(context)
    {
    }
}
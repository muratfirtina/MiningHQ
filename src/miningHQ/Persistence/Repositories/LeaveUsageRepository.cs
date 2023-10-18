using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LeaveUsageRepository : EfRepositoryBase<LeaveUsage, Guid, MiningHQDbContext>, ILeaveUsageRepository
{
    public LeaveUsageRepository(MiningHQDbContext context) : base(context)
    {
    }
}
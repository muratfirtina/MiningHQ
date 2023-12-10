using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class LeaveTypeRepository : EfRepositoryBase<LeaveType, Guid, MiningHQDbContext>, ILeaveTypeRepository
{
    public LeaveTypeRepository(MiningHQDbContext context) : base(context)
    {
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class TimekeepingRepository : EfRepositoryBase<Timekeeping, Guid, MiningHQDbContext>, ITimekeepingRepository
{
    public TimekeepingRepository(MiningHQDbContext context) : base(context)
    {
    }
}
using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DailyWorkDataRepository : EfRepositoryBase<DailyWorkData, Guid, MiningHQDbContext>, IDailyWorkDataRepository
{
    public DailyWorkDataRepository(MiningHQDbContext context) : base(context)
    {
    }
}
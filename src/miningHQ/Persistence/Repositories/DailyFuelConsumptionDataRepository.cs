using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class DailyFuelConsumptionDataRepository : EfRepositoryBase<DailyFuelConsumptionData, Guid, MiningHQDbContext>, IDailyFuelConsumptionDataRepository
{
    public DailyFuelConsumptionDataRepository(MiningHQDbContext context) : base(context)
    {
    }
}
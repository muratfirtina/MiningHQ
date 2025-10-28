using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuarryProductionRepository : EfRepositoryBase<QuarryProduction, Guid, MiningHQDbContext>, IQuarryProductionRepository
{
    public QuarryProductionRepository(MiningHQDbContext context) : base(context)
    {
    }
}

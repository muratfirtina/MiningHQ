using Application.Services.Repositories;
using Domain.Entities;
using Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class QuarryRepository : EfRepositoryBase<Quarry, Guid, MiningHQDbContext>, IQuarryRepository
{
    public QuarryRepository(MiningHQDbContext context) : base(context)
    {
    }
}